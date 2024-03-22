<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="e_rehistro.Default" %>

<asp:Content ID="AuthenticationPage" ContentPlaceHolderID="Body" runat="server">
    <div class="page-container">
        <div id="authentication-slider">
            <img src="assets/authentication-slider.png" alt="logo_ng_pinas">
        </div>
        <form runat="server">
            <div id="signin-container">
                <div class="website-title">
                    <img src="assets/e-rehistro-icon.png" alt="e_rehistro_icon">
                    <p>E-Rehistro</p>
                </div>
                <div id="signin-message">
                    <p>Welcome back!</p>
                </div>
                <div id="signin-email" class="input-field">
                    <asp:TextBox ID="txtSigninEmail" runat="server" CssClass="txtInputField" required=""></asp:TextBox>
                    <label for="txtSigninEmail">Email</label>
                </div>
                <div id="signin-password" class="input-field">
                    <asp:TextBox ID="txtSigninPassword" runat="server" TextMode="Password" CssClass="txtInputField" required=""></asp:TextBox>
                    <label for="txtSigninPassword">Password</label>
                </div>
                <div id="signin-button">
                    <button type="submit" id="sign-in-button">Sign In</button>
                </div>
                <div id="switch-to-signup">
                    <p>Need an account? <b id="sign-up">Sign Up</b></p>
                </div>
            </div>
            <div id="signup-container">
                <div class="website-title">
                    <img src="assets/e-rehistro-icon.png" alt="e_rehistro_icon">
                    <p>E-Rehistro</p>
                </div>
                <div id="signup-message">
                    <p>Create an account!</p>
                    <p style="font-size: 20px; font-weight: 400;">Let's get started</p>
                </div>
                <div id="signup-email" class="input-field">
                    <asp:TextBox ID="txtSignupEmail" runat="server" CssClass="txtInputField" required=""></asp:TextBox>
                    <label for="txtSignupEmail">Email</label>
                </div>
                <div id="signup-password" class="input-field">
                    <asp:TextBox ID="txtSignupPassword" runat="server" TextMode="Password" CssClass="txtInputField" required=""></asp:TextBox>
                    <label for="txtSignupPassword">Password</label>
                </div>
                <div id="signup-confirm-password" class="input-field">
                    <asp:TextBox ID="txtSignupConfirmPassword" runat="server" TextMode="Password" CssClass="txtInputField" required=""></asp:TextBox>
                    <label for="txtSignupPassword">Confirm Password</label>
                </div>
                <div id="signup-button">
                    <button type="submit" id="sign-up-button">Sign Up</button>
                </div>
                <div id="switch-to-signin">
                    <p>Already have an account? <b id="sign-in">Sign In</b></p>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
