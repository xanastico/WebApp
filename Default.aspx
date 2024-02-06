<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Teste_WebApp.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5dc;
            margin: 0;
            padding: 0;
        }
        form {
            width: 80%;
            margin: 20px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 8px;
        }
        input[type="text"],
        input[type="password"],
        input[type="button"] {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }
        input[type="button"] {
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
        }
        input[type="button"]:hover {
            background-color: #45a049;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="LoginPanel" runat="server">
                <h1>Login</h1>
                <asp:Label ID="LoginStatusLabel" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                <label for="UsernameTextBox">Username</label>
                <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
                <label for="PasswordTextBox">Password</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </asp:Panel>

            <asp:Panel ID="ManageDataPanel" runat="server" Visible="false">
                <h1>Display Data</h1>
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>

                <!-- Add Vehicle Section -->
                <br /><br />
                <h1>Insert Vehicle</h1>
                <label for="MarcaTextBox">Marca</label>
                <asp:TextBox ID="MarcaTextBox" runat="server"></asp:TextBox>
                <label for="TipoTextBox">Tipo</label>
                <asp:TextBox ID="TipoTextBox" runat="server"></asp:TextBox>
                <label for="QuantidadeTextBox">Quantidade</label>
                <asp:TextBox ID="QuantidadeTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="InsertVehicleButton" runat="server" Text="Insert Vehicle" OnClick="InsertVehicleButton_Click" />

                <!-- Update Vehicle Section -->
                <br /><br />
                <h1>Update Vehicle</h1>
                <label for="UpdateIDTextBox">ID</label>
                <asp:TextBox ID="UpdateIDTextBox" runat="server"></asp:TextBox>
                <label for="UpdateTipoTextBox">New Tipo</label>
                <asp:TextBox ID="UpdateTipoTextBox" runat="server"></asp:TextBox>
                <label for="UpdateQuantidadeTextBox">New Quantidade</label>
                <asp:TextBox ID="UpdateQuantidadeTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="UpdateVehicleButton" runat="server" Text="Update Vehicle" OnClick="UpdateVehicleButton_Click" />

                <!-- Delete Vehicle Section -->
                <br /><br />
                <h1>Delete Vehicle</h1>
                <label for="DeleteIdTextBox">ID do Veículo</label>
                <asp:TextBox ID="DeleteIdTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="DeleteVehicleButton" runat="server" Text="Delete Vehicle" OnClick="DeleteVehicleButton_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
