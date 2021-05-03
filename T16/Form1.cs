using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int n;
        PictureBox[,] P;
        string color = "r", k = "", B1 = "", B2 = "",k2="";

        private void label4_Click(object sender, EventArgs e)
        {
            W.Visible = false;
            for (int h = 0; h < n; h++)
                for (int l = 0; l < n; l++)
                {
                    if (h < (n / 2) - 1 && P[h, l].BackColor == Color.Black) { P[h, l].Image = Properties.Resources.r; P[h, l].Name = h + " " + l + " r"; }
                    else if (h > (n / 2) && P[h, l].BackColor == Color.Black)
                    {
                        P[h, l].Image = Properties.Resources.g; P[h, l].Name = h + " " + l + " g";
                    }
                    if (h == ((n / 2) - 1) || h == (n / 2)) P[h, l].Image = null;
                }
            labelp1.Text = "0";
            labelp2.Text = "0";
            Player1.ReadOnly = false;
            Player2.ReadOnly = false;
            Player1.Text = "";
            Player2.Text = "";
            Player1.BackColor = Color.White;
            Player2.BackColor = Color.White;
            Player1.ForeColor = Color.Black;
            Player2.ForeColor = Color.Black;
            red = 0;
            green = 0;
        }

        private void Player1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                TextBox t = sender as TextBox;
                if (t.Text != "" && Player1.Text != Player2.Text)
                {
                    t.ReadOnly = true;
                    if (t.Name == "Player2")
                        t.BackColor = Color.Green;
                    else
                        t.BackColor = Color.Red;
                    t.ForeColor = Color.White;
                }
                else
                {
                    if (t.Text == "")
                        MessageBox.Show("Player Name can't be blank");
                    if (Player1.Text == Player2.Text)
                        MessageBox.Show("The players can't have the same name");
                }
            }

        }

        int green =0,red=0;
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Parent = this;
            panel1.BringToFront();
            play.Click += (sender2, e2) => {
                panel1.Visible = false;

            };
            play.MouseHover += (sender3, e3) => {
                Label l = sender3 as Label;
                l.ForeColor = Color.Black;
                panel1.BackColor = Color.White;
            };
            play.MouseLeave += (sender3, e3) => {
                Label l = sender3 as Label;
                l.ForeColor = Color.White;
                panel1.BackColor = Color.Black;
            };
            n = 8;
            P = new PictureBox[n, n];
            int left = 2, top = 2;
            Color[] colors = new Color[] { Color.White, Color.Black };
            for (int i = 0; i < n; i++)
            {
                left = 2;
                if (i % 2 == 0) { colors[0] = Color.White; colors[1] = Color.Black; }
                else { colors[0] = Color.Black; colors[1] = Color.White; }

                for (int j = 0; j < n; j++)
                {
                    P[i, j] = new PictureBox();
                    P[i, j].BackColor = colors[(j % 2 == 0) ? 1 : 0];
                    P[i, j].Location = new Point(left, top);
                    P[i, j].Size = new Size(60, 60);
                    left += 60;
                    P[i, j].Name = i + " " + j;
                    if (i < (n / 2) - 1 && P[i, j].BackColor == Color.Black) { P[i, j].Image = Properties.Resources.r; P[i, j].Name += " r"; }
                    else if (i > (n / 2) && P[i, j].BackColor == Color.Black)
                    {
                        P[i, j].Image = Properties.Resources.g; P[i, j].Name += " g";
                    }
                    P[i, j].SizeMode = PictureBoxSizeMode.CenterImage;
                    P[i, j].MouseHover += (sender2, e2) =>
                    {
                        PictureBox p = sender2 as PictureBox;
                        if (p.Image != null) p.BackColor = Color.FromArgb(255, 64, 64, 64);
                    };
                    P[i, j].MouseLeave += (sender2, e2) =>
                    {
                        PictureBox p = sender2 as PictureBox;
                        if (p.Image != null) p.BackColor = Color.Black;
                    };

                    P[i, j].Click += (sender3, e3) =>
                    {
                        if (Player1.ReadOnly && Player2.ReadOnly)
                        {
                            PictureBox p = sender3 as PictureBox;
                            if (p.Image != null)
                            {
                                int c = -1, x, y;
                                F();
                                if (p.Name.Split(' ')[2] == "b")
                                {
                                    if (color == "r") color = "g";
                                    else color = "r";
                                    x = Convert.ToInt32(k.Split(' ')[0]);
                                    y = Convert.ToInt32(k.Split(' ')[1]);
                                    B1 = "";
                                    B2 = "";
                                    if (k.Split(' ')[2] == "r")
                                    {
                                        p.Image = Properties.Resources.r;
                                        p.Name = p.Name.Replace("b", "r");
                                    }
                                    else
                                    if (k.Split(' ')[2] == "g")
                                    {
                                        p.Image = Properties.Resources.g;
                                        p.Name = p.Name.Replace("b", "g");
                                    }
                                    P[x, y].Image = null;
                                    if (k2 != "")
                                    {
                                        x = Convert.ToInt32(k2.Split(' ')[0]);
                                        y = Convert.ToInt32(k2.Split(' ')[1]);
                                        P[x, y].Image = null;
                                        if (k2.Split(' ')[2] == "r") red++;
                                        else green++;
                                        labelp1.Text = green + "";
                                        labelp2.Text = red + "";
                                        if (red >= 12)
                                        {
                                            labelw.Text = "You win " + Player2.Text;
                                            W.Visible = true;
                                        }
                                        else
                                        if (green >= 12)
                                        {
                                            labelw.Text = "You win " + Player1.Text;
                                            W.Visible = true;
                                        }
                                            k2 = "";
                                    }

                                }
                                else
                                if (p.Name.Split(' ')[2] == color)
                                {
                                    x = Convert.ToInt32(p.Name.Split(' ')[0]);
                                    y = Convert.ToInt32(p.Name.Split(' ')[1]);
                                    k = p.Name;
                                    if (p.Name.Split(' ')[2] == "r") c = 1;
                                    try
                                    {
                                        if (P[x + c, y + 1].Image == null)
                                        {
                                            P[x + c, y + 1].Image = Properties.Resources.b;
                                            P[x + c, y + 1].Name = (x + c) + " " + (y + 1) + " b";
                                            B1 = (x + c) + " " + (y + 1);
                                        }
                                        else
                                            if (P[x + c, y + 1].Name.Split(' ')[2] != p.Name.Split(' ')[2] && P[x + (c * 2), y + 2].Image == null)
                                        {
                                            P[x + (c * 2), y + 2].Image = Properties.Resources.b;
                                            P[x + (c * 2), y + 2].Name = (x + (c * 2)) + " " + (y + 2) + " b";
                                            B1 = (x + (c * 2)) + " " + (y + 2);
                                            k2 = (x + c) + " " + (y + 1) + " " + P[x + c, y + 1].Name.Split(' ')[2];
                                        }

                                    }
                                    catch { }
                                    try
                                    {
                                        if (P[x + c, y - 1].Image == null)
                                        {
                                            P[x + c, y - 1].Image = Properties.Resources.b;
                                            P[x + c, y - 1].Name = (x + c) + " " + (y - 1) + " b";
                                            B2 = (x + c) + " " + (y - 1);
                                        }
                                        else
                                            if (P[x + c, y - 1].Name.Split(' ')[2] != p.Name.Split(' ')[2] && P[x + (c * 2), y - 2].Image == null)
                                        {
                                            P[x + (c * 2), y - 2].Image = Properties.Resources.b;
                                            P[x + (c * 2), y - 2].Name = (x + (c * 2)) + " " + (y - 2) + " b";
                                            B2 = (x + (c * 2)) + " " + (y - 2);
                                            k2 = (x + c) + " " + (y - 1) + " " + P[x + c, y - 1].Name.Split(' ')[2];
                                        }

                                    }
                                    catch { }
                                }
                            }
                        }
                    };

                    G.Controls.Add(P[i, j]);
                }
                top += 60;
            }


        }
        public void F()
        {
            if (B1 != "")
            {
                int x, y;
                x = Convert.ToInt32(B1.Split(' ')[0]);
                y = Convert.ToInt32(B1.Split(' ')[1]);
                P[x, y].Image = null;
            }
            if (B2 != "")
            {
                int x, y;
                x = Convert.ToInt32(B2.Split(' ')[0]);
                y = Convert.ToInt32(B2.Split(' ')[1]);
                P[x, y].Image = null;
            }
        }

    }
}
