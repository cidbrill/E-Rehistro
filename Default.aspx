<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="e_rehistro.Default" %>

<asp:Content ID="AuthenticationPage" ContentPlaceHolderID="AuthenticationPage" runat="server">     
    <div id="authentication-page" class="page-container">
        <div id="authentication-slider">
            <img src="assets/authentication-slider.png" alt="logo_ng_pinas">
        </div>
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
                <asp:Button ID="btnSignInButton" runat="server" Text="Sign In" CssClass="btnSubmitForm" />
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
                <asp:Button ID="btnSignUpButton" runat="server" Text="Sign Up" CssClass="btnSubmitForm" />
            </div>
            <div id="switch-to-signin">
                <p>Already have an account? <b id="sign-in">Sign In</b></p>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="HomePage" ContentPlaceHolderID="HomePage" runat="server">
    <div id="home-page" class="page-container" style="background-image: url('assets/home-page.png'); background-repeat: no-repeat; background-size: cover; background-position: center; background-attachment: fixed; display: flex; flex-direction: column;">
        <div id="home-message">
            <p>Welcome to E-Rehistro,</p>
            <p style="font-size: 25px; font-family: 'Pragati Narrow';">where Filipinos can easily register for their Voter's ID online. Join us in streamlining the process and empowering every citizen's voice.</p>
        </div>
        <div id="shortcut-buttons">
            <asp:Button ID="btnViewStatus" runat="server" Text="View Status" CssClass="btnShortcutButton" />
            <div style="width: 150px; height: 50px;"></div>
            <asp:Button ID="btnRegisterNow" runat="server" Text="Register Now" CssClass="btnShortcutButton" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="NewsAndEventsPage" ContentPlaceHolderID="NewsAndEventsPage" runat="server">
    <div id="news-page" class="page-container" style="background-color: #FAF7EE">
        <div class="news-panel">
            <img src="assets/Nationwide.png" alt="article_image" />
            <p class="article-headline">E-Rehistro Launches Nationwide Voter ID Registration Drive, Bringing Convenience</p>
            <p class="article-preview">E-Rehistro, the leading online platform for voter registration in the Philippines, initiates a comprehensive nationwide campaign to...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
        <div class="news-panel">
            <img src="assets/Biometric.png" alt="article_image" />
            <p class="article-headline">E-Rehistro Introduces Biometric Verification for Enhanced Voter ID Security</p>
            <p class="article-preview">E-Rehistro raises the bar in voter ID security by implementing biometric verification, ensuring the integrity of voter registrations and...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
        <div class="news-panel">
            <img src="assets/Partners.png" alt="article_image" />
            <p class="article-headline">E-Rehistro Partners with Local Governments</p>
            <p class="article-preview">In a landmark collaboration, E-Rehistro joins forces with local government units nationwide to widen the reach of voter registration...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
        <div class="news-panel">
            <img src="assets/unloaded-image.png" alt="article_image" />
            <p class="article-headline">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor</p>
            <p class="article-preview">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor sit amet metus...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
        <div class="news-panel">
            <img src="assets/unloaded-image.png" alt="article_image" />
            <p class="article-headline">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor</p>
            <p class="article-preview">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor sit amet metus...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
        <div class="news-panel">
            <img src="assets/unloaded-image.png" alt="article_image" />
            <p class="article-headline">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor</p>
            <p class="article-preview">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tempor sit amet metus...</p>
            <asp:Button runat="server" Text="READ MORE" CssClass="btnReadMoreButton" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="AboutPage" ContentPlaceHolderID="AboutPage" runat="server">
    <div id="about-page" class="page-container" style="background-color: #FAF7EE">
    </div>
</asp:Content>

<asp:Content ID="ContactsPage" ContentPlaceHolderID="ContactsPage" runat="server">
    <div id="contacts-page" class="page-container" style="background-color: #FAF7EE">
    </div>
</asp:Content>
