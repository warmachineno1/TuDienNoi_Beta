using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using SpeechLib;
using System.Net;
using System.IO;

namespace DoAnCK_TDN
{
    public partial class MainApp : Form
    {
        
        public MainApp()
        {
            InitializeComponent();
            
        }
        

        private void LoadListBoxData()
        {
            try
            {
                // Lấy các items có trong database ra
                DataTable ds = SqlHelper.ExecuteDataset(SQLstring.strCon, "TuVung_Chon").Tables[0];
                // Đổ items ra listbox
                listBox1.DataSource = ds;
                listBox1.DisplayMember = "Từ_Tiếng_Anh";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Eng = textBox1.Text.Trim();
                // Gía trị tìm đc sẽ chứa trong 1 table tạm để đưa ra textbox
                //DataTable ds = SqlHelper.ExecuteDataset(SQLstring.strCon, "TuVung_Them", Eng).Tables[0];
                DataTable ds = SqlHelper.ExecuteDataset(SQLstring.strCon, "TuVung_TraTu", Eng).Tables[0];
                // Kiểm tra từ này có tồn tại trong csdl chưa
                if (ds.Rows.Count > 0)
                {
                    textBox1.Text = ds.Rows[0]["Từ_Tiếng_Anh"].ToString();
                    NghiaLabel.Text = ds.Rows[0]["Dịch_Nghĩa_Chi_Tiết"].ToString();
                    DongNghiaLabel.Text = ds.Rows[0]["Từ_Đồng_Nghĩa"].ToString();
                    TraiNghiaLabel.Text = ds.Rows[0]["Từ_Trái_Nghĩa"].ToString();
                }
                else
                {
                    MessageBox.Show("Rất tiếc, từ bạn tra hiện không có !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadListBoxData();               
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                // Biến temp tạm chứa các từ mà ng` dùng đang click vào
                string temp = listBox1.Text;
                DataTable ds = SqlHelper.ExecuteDataset(SQLstring.strCon, "TuVung_ChonTuListbox", temp).Tables[0];
                textBox1.Text = listBox1.Text;
                if (ds.Rows.Count > 0)
                {
                    NghiaLabel.Text = ds.Rows[0]["Dịch_Nghĩa_Chi_Tiết"].ToString();
                    DongNghiaLabel.Text = ds.Rows[0]["Từ_Đồng_Nghĩa"].ToString();
                    TraiNghiaLabel.Text = ds.Rows[0]["Từ_Trái_Nghĩa"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
                
        private void MainApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Management manager = new Management();
            manager.Show();
            
        }       

            private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Music music = new Music();
            music.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SpVoice Speaker = new SpVoice();
            Speaker.Speak(textBox1.Text, SpeechVoiceSpeakFlags.SVSFDefault);
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Video video = new Video();
            video.Show();
        }
    }
}
