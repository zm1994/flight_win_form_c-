using FlightConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace ClientWinForms
{
    public partial class Form1 : Form
    {
        FlightController flightController;
        List<Direction> directionWithTransfers;

        public Form1()
        {
            InitializeComponent();
            flightController = new FlightController();
            directionWithTransfers = new List<Direction>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl.DragButton = MouseButtons.Left;
            //set center  mouse zoom
            gMapControl.MouseWheelZoomType =
                GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            //set using google mapp provider
            gMapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //set initial position by keyword
            gMapControl.SetPositionByKeywords("UA");
        }

        private void ShowAvailableDirections(List<Direction> directions)
        {
            gMapControl.Overlays.Clear();
            GMapOverlay routes = new GMapOverlay("routes");
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            foreach(var direction in directions)
            {
                //draw lines for direction
                ShowDirection(direction, routes);
                //show labels in every airport point in direction
                ShowMarkersByDirection(direction, markersOverlay);
            }
            gMapControl.Overlays.Add(routes);
            gMapControl.Overlays.Add(markersOverlay);
        }

        private void ShowDirection(Direction direction, GMapOverlay routes) 
        {
            for (int i = 0; i < direction.Airports.Count - 1; i++)
                AddRoute(direction.Airports[i], direction.Airports[i + 1], routes);
        }

        private void ShowMarkersByDirection(Direction direction, GMapOverlay markers) 
        {
           foreach(Airport airport in direction.Airports)
               AddMarker(airport, markers);
        }

        private void AddRoute(Airport departure, Airport arrival, GMapOverlay routes) 
        {
            //create list points
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(departure.location.lat, departure.location.lon));
            points.Add(new PointLatLng(arrival.location.lat, arrival.location.lon));
            GMapRoute route = new GMapRoute(points, departure.code_airport + " " + arrival.code_airport);
            route.Stroke = new Pen(Color.Aqua, 2);
            routes.Routes.Add(route);
            gMapControl.UpdateRouteLocalPosition(route);
        }

        private void AddMarker(Airport airport, GMapOverlay markersOverlay) 
        {   
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(airport.location.lat, airport.location.lon), GMarkerGoogleType.orange);
            marker.ToolTip = new GMapToolTip(marker);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.Size = new System.Drawing.Size(21, 21);
            marker.Offset = new System.Drawing.Point(-10, -15);
            marker.ToolTip.Font = new Font("Arial", 9, FontStyle.Bold);
            marker.ToolTipText = airport.code_airport;
            markersOverlay.Markers.Add(marker);
            gMapControl.UpdateMarkerLocalPosition(marker);
        }

        private void DisableControls(Control con)
        {
            foreach (Control c in con.Controls)
                DisableControls(c);
            con.Enabled = false;
        }

        private void EnableControls(Control con)
        {
            foreach (Control c in con.Controls)
                EnableControls(c);
            con.Enabled = true;
        }

        private async void txtBoxFrom_KeyUp(object sender, KeyEventArgs e)
      {
            if(e.KeyCode == Keys.Enter) 
            {
                if (txtBoxFrom.Text.Length > 0)
                {
                    try
                    {
                        DisableControls(this);
                        this.Text = "Please wait...";
                        List<Direction> directions = await Task.Factory.StartNew(() => flightController.GetAvailableDirections(txtBoxFrom.Text));
                        this.Text = "Best Fly";
                        EnableControls(this);
                        lstBoxVariants.Items.Clear();
                        ShowAvailableDirections(directions);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private async void txtBoxTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBoxFrom.Text.Length > 0 && txtBoxTo.Text.Length > 0)
                {
                    try
                    {
                        DisableControls(this);
                        this.Text = "Please wait...";
                        directionWithTransfers = await Task.Factory.StartNew(() => flightController.GetDirectionsWithTransfers(txtBoxFrom.Text, txtBoxTo.Text));
                        this.Text = "Best Fly";
                        EnableControls(this);
                        lstBoxVariants.Items.Clear();
                        foreach (var direction in directionWithTransfers) 
                        {
                            lstBoxVariants.Items.Add(direction.ToString());
                        }
                        if(directionWithTransfers.Count > 0)
                            lstBoxVariants.SetSelected(0, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void lstBoxVariants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxVariants.SelectedIndex != -1) 
            {
                gMapControl.Overlays.Clear();
                GMapOverlay routes = new GMapOverlay("routes");
                GMapOverlay markersOverlay = new GMapOverlay("markers");
                Direction direction = directionWithTransfers[lstBoxVariants.SelectedIndex];
                ShowDirection(direction, routes);
                ShowMarkersByDirection(direction, markersOverlay);
                gMapControl.Overlays.Add(routes);
                gMapControl.Overlays.Add(markersOverlay);
            }
        }

        private void gMapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            item.Size = new System.Drawing.Size(25, 25);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtBoxFrom.Text.Length > 0 && txtBoxTo.Text.Length > 0) && (numAdults.Value > 0 || numChild.Value > 0))
                {
                    string arrivalDate = "";
                    if (monthCalendar.BoldedDates.Length == 2)
                        arrivalDate = monthCalendar.BoldedDates[1].ToShortDateString();
                    //create avia sales searcher
                    MetaSearchController aviaSalesSearcher = new AviaSalesMetaSearchController(txtBoxFrom.Text,
                        txtBoxTo.Text, (uint)numAdults.Value, (uint)numChild.Value, (uint)numInfants.Value,
                        monthCalendar.BoldedDates[0].ToShortDateString(), arrivalDate);
                    //create tripmydream searcher
                    MetaSearchController tripmyDreamSearcher = new TripMyDreamMetaSearchController(txtBoxFrom.Text,
                        txtBoxTo.Text, (uint)numAdults.Value, (uint)numChild.Value, (uint)numInfants.Value,
                        monthCalendar.BoldedDates[0].ToShortDateString(), arrivalDate);
                    //start searchers
                    tripmyDreamSearcher.Search();
                    aviaSalesSearcher.Search();
                } 
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void monthCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            MonthCalendar.HitTestInfo info = monthCalendar.HitTest(e.Location);
            if (info.HitArea == MonthCalendar.HitArea.Date)
            {
                if (monthCalendar.BoldedDates.Contains(info.Time))
                    monthCalendar.RemoveBoldedDate(info.Time);
                else
                    monthCalendar.AddBoldedDate(info.Time);
                if (monthCalendar.BoldedDates.Count() > 2) //if count choiced dates is more than 2 clear calendar
                    monthCalendar.RemoveAllBoldedDates();
                monthCalendar.UpdateBoldedDates();
            }
        }
    }
}
