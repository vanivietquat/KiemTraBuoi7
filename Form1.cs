using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi7KT
{
    public partial class Form1 : Form
    {
        SanPhamDB db;
        public Form1()
        {
            InitializeComponent();
            db = new SanPhamDB();
            List<Sanpham> sanphams = db.Sanphams.ToList();

            List<LoaiSP> loais = db.LoaiSPs.ToList();
            BindProduce(sanphams);
            BindingCategory(loais);

        }

        private void BindingCategory(List<LoaiSP> loais)
        {

            this.cmbLoaiSP.Items.Clear();
            this.cmbLoaiSP.DataSource = loais;
            this.cmbLoaiSP.DisplayMember = "TenLoai";
            this.cmbLoaiSP.ValueMember = "Maloai";

        }

        private void BindProduce(List<Sanpham> sanphams)
        {
            List<LoaiSP> loais = db.LoaiSPs.ToList();
            dgvSP.Rows.Clear();
            foreach (var items in sanphams)
            {
                int index = dgvSP.Rows.Add();
                dgvSP.Rows[index].Cells[0].Value = items.MaSP;
                dgvSP.Rows[index].Cells[1].Value = items.TenSP;
                dgvSP.Rows[index].Cells[2].Value = items.NgayNhap;
                dgvSP.Rows[index].Cells[3].Value = items.MaLoai;

            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                    string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                    cmbLoaiSP.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<Sanpham> Producelist = db.Sanphams.ToList();

                var existingProduce = Producelist.FirstOrDefault(s => s.MaSP == txtMaSP.Text);

                if (existingProduce == null)
                {

                    var newSanPham = new Sanpham
                    {
                        MaSP = txtMaSP.Text,
                        TenSP = txtTenSP.Text,
                        NgayNhap = dtNgayNhap.Value,
                        

                    };
                    db.Sanphams.Add(newSanPham);
                    db.SaveChanges();
                    BindProduce(db.Sanphams.ToList());
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvSP.Rows[e.RowIndex];
                txtMaSP.Text = selectedRow.Cells[0].Value.ToString();
                txtTenSP.Text = selectedRow.Cells[1].Value.ToString();
                dtNgayNhap.Text = selectedRow.Cells[2].Value.ToString();
                cmbLoaiSP.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sanpham> sanphams = db.Sanphams.ToList();
                var student = sanphams.FirstOrDefault(s => s.MaSP == txtMaSP.Text);

                if (student != null)
                {
                    db.Sanphams.Remove(student);
                    db.SaveChanges();
                    BindProduce(db.Sanphams.ToList());

                    MessageBox.Show(" xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu : {ex.Message}", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
               
                this.Close();
            }
        }
    }
}




