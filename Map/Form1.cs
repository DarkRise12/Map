using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using Map.Objects;
using Map.Markers;

namespace Map
{
    public partial class Form1 : Form
    {
        private string _connectionString;
        private SqlConnection _conn;
        private List<Marker> _markers = new List<Marker>();
        List<CustomGMapMarker> _mapMarkers = new List<CustomGMapMarker>();
        GMapOverlay _markerOverlay = new GMapOverlay("markers");

        public Form1()
        {
            InitializeComponent();
            SqlConnect();
        }

        private void SqlConnect()
        {
            _connectionString = "Data Source=10.55.205.31;Initial Catalog=MapTest;User ID=sa;Password=Wen76Coda";
            _conn = new SqlConnection(_connectionString);
            _conn.Open();
            UpdateData();
        }

        private void UpdateData()
        {
            string select_all = "SELECT * FROM mark";
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand(select_all, _conn);
                da.Fill(dt);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
            }
            
            FillList(dt);
        }

        private void FillList(DataTable dt)
        {
            _markers.Clear();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["Id"]);
                string name = dt.Rows[i]["Name"].ToString();
                double lat = Convert.ToDouble(dt.Rows[i]["Lat"]);
                double lng = Convert.ToDouble(dt.Rows[i]["Lng"]);

                Marker marker = new Marker(id, name, new PointLatLng(lat, lng));

                _markers.Add(marker);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MaxZoom = 60;
            gMapControl1.MinZoom = 0;
            gMapControl1.Zoom = 17;
            gMapControl1.MapProvider = YandexMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.Position = new PointLatLng(54.9860956, 82.9050593);

            foreach(Marker i in _markers)
            {
                CustomGMapMarker mark = new CustomGMapMarker(i.Id, i.Point);

                _mapMarkers.Add(mark);
                _markerOverlay.Markers.Add(mark);
            }
            gMapControl1.Overlays.Add(_markerOverlay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedCellCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            {
                MessageBox.Show("Ни одна клетка не выделена");
            }
            else
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int idFound = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);
                int rowsAmount = dataGridView1.Rows.Count;
                CustomGMapMarker mrk = _mapMarkers.FirstOrDefault(x => x.Id == idFound);

                using (SqlCommand command = new SqlCommand("DELETE FROM mark WHERE Id = " + idFound, _conn))
                {
                    command.ExecuteNonQuery();
                }

                if(mrk != null)
                {
                    _markerOverlay.Markers.Remove(mrk);
                }

                if(rowsAmount != 1)
                {
                    dataGridView1.Rows.RemoveAt(rowIndex);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            if(form.DialogResult == DialogResult.OK )
            {
                using (SqlCommand command = new SqlCommand($"INSERT INTO mark VALUES ('{form.Name}',{form.Lat},{form.Lng})", _conn))
                {
                    command.ExecuteNonQuery();
                }

                UpdateData();
                
                CustomGMapMarker mark = new CustomGMapMarker(_markers.Last().Id, _markers.Last().Point);

                _mapMarkers.Add(mark);
                _markerOverlay.Markers.Add(mark);
                
                gMapControl1.Overlays.Add(_markerOverlay);
            }
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedCellCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            {
                MessageBox.Show("Ни одна клетка не выделена");
            }
            else
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int idFound = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);
                int rowsAmount = dataGridView1.Rows.Count;
                CustomGMapMarker mrk = _mapMarkers.FirstOrDefault(x => x.Id == idFound);
                
                gMapControl1.Position = mrk.Position;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _conn.Close();
        }
    }
}