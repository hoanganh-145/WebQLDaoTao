<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckLogin.aspx.cs" Inherits="WebQLDaoTao.CheckLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2 class="text-danger">Không có quyền truy cập trang này!</h2>
            <a href="Login.aspx" class="btn btn-primary">Đăng nhập lại</a>
        </div>
    </form>
</body>
</html>