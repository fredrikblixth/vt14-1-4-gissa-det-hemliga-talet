<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GissaTalet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gissa det hemliga talet</title>
    <link href="css/normalize.css" rel="stylesheet" />
    <link href="css/foundation.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="small-8 columns">
                    <h1>Det hemliga talet</h1>
                </div>
            </div>
            <div class="row">
                <div class="small-4 columns">
                    <%-- Label, textbox and button --%>
                    <asp:Label ID="GuessNumberLabel" runat="server" Text="Gissa det hemliga talet mellan 1 och 100" CssClass="label"></asp:Label>
                    <asp:TextBox ID="GuessNumberTextbox" runat="server"></asp:TextBox>
                    <asp:Button ID="GuessButton" runat="server" Text="Gissa!" OnClick="GuessButton_Click" CssClass="button tiny" />

                    <%-- Validation --%>
                    <asp:RequiredFieldValidator ID="NumberRequired" runat="server" 
                        ErrorMessage="Ett tal måste anges" 
                        ControlToValidate="GuessNumberTextbox"
                        Display="None"></asp:RequiredFieldValidator>

                    <asp:RangeValidator ID="NumberRangeValidator" runat="server" 
                        ErrorMessage="Ett heltal mellan 1-100 måste anges" 
                        ControlToValidate="GuessNumberTextbox" 
                        MaximumValue="100" MinimumValue="1" Type="Integer" 
                        Display="None"></asp:RangeValidator>

                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </div>
            </div>
            <%-- Result --%>
            <asp:PlaceHolder ID="ResultPlaceholder" runat="server" Visible="false">
                <div class="row">
                    <div class="small-4 columns">
                        <asp:Label ID="GuessedNumbersLabel" runat="server" Text=""></asp:Label>  
                        <asp:Panel ID="SuccessPanel" runat="server" CssClass="alert-box success" Visible="false">
                            <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="NoMoreGuessesPanel" runat="server" CssClass="alert-box alert" Visible="false">
                            <asp:Label ID="NoMoreGuessesLabel" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                        <asp:Button ID="NewSecretNumberButton" runat="server" Text="Slumpa nytt hemligt tal!" CssClass="button tiny" Visible="false" OnClick="NewSecretNumberButton_Click"/>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
    </form>
    <script src="Scripts/js/vendor/jquery.js"></script>
    <script src="Scripts/js/foundation.min.js"></script>
    <script>
        $(document).foundation();
    </script>
</body>
</html>
