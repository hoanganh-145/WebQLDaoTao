using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebQLDaoTao.Models;

namespace WebQLDaoTao
{
    public partial class QLKhoa : System.Web.UI.Page
    {
        KhoaDAO khDAO = new KhoaDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //lien ket du lieu cho gvKhoa
                gvKhoa.DataSource = khDAO.getAll();
                gvKhoa.DataBind();
            }
        }

        private void LoadData()
        {
            gvKhoa.DataSource = khDAO.getAll();
            gvKhoa.DataBind();
        }

        private void ShowMessage(string message, bool isError = false)
        {
            // Use a more elegant approach for displaying messages
            string script = $"Swal.fire({{ icon: '{(isError ? "error" : "success")}', title: '{(isError ? "Lỗi" : "Thành công")}', text: '{message}' }});";
            ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", script, true);
        }

        private void ClearForm()
        {
            txtMakh.Text = string.Empty;
            txtTenkh.Text = string.Empty;
        }

        protected void gvKhoa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKhoa.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvKhoa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKhoa.EditIndex = -1;
            LoadData();
        }

        protected void gvKhoa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string makh = gvKhoa.DataKeys[e.RowIndex].Value.ToString();
                Khoa kh = new Khoa { MaKH = makh };
                khDAO.Delete(kh);
                LoadData();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Không thể xoá khoa này')</script>");
            }
        }

        protected void gvKhoa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string makh = gvKhoa.DataKeys[e.RowIndex].Value.ToString();
            string tenkh = ((TextBox)gvKhoa.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            Khoa khUpdate = new Khoa { MaKH = makh, TenKH = tenkh };
            khDAO.Update(khUpdate);
            gvKhoa.EditIndex = -1;
            LoadData();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we should use txtMaKH or txtMakh based on the designer file
                string makh = txtMakh.Text;
                string tenkh = txtTenkh.Text;

                if (khDAO.findById(makh) != null)
                {
                    Response.Write("<script>alert('Mã khoa đã tồn tại. Vui lòng nhập mã khác.')</script>");
                    return;
                }

                Khoa khInsert = new Khoa { MaKH = makh, TenKH = tenkh };
                khDAO.Insert(khInsert);
                LoadData();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Thao tác thêm khoa không thành công.')</script>");
            }
        }

        protected void gvKhoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKhoa.PageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}