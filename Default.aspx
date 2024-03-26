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
            <div id="shortcut-buttons">
                <asp:Button ID="btnViewStatus" runat="server" Text="View Status" CssClass="btnShortcutButton" />
                <div style="width: 150px; height: 50px;"></div>
                <asp:Button ID="btnRegisterNow" runat="server" Text="Register Now" CssClass="btnShortcutButton" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="RegistrationPage" ContentPlaceHolderID="RegistrationPage" runat="server">
    <div id="registration-page" class="page-container" style="background-color: #FAF7EE;">
        <div class="steps-container">
            <div class="instruction">
                <p style="font-size: 35px; font-weight: 700; color: #294278;">How it works</p>
                <p style="font-size: 20px; color: #736C6D;">Get started with 3 easy steps</p>
            </div>
            <p class="step-count">1</p>
            <p class="step-title">Complete Personal Details</p>
            <div class="step-description-container">
                <p class="step-description">Fill out the Personal Information Form</p>
            </div>
            <div class="step-container-button">
                <asp:Button ID="btnCompleteFormButton" runat="server" Text="COMPLETE FORM" CssClass="btnRegistrationButton" />
            </div>
        </div>
        <div class="next-step-indicator">
            <img src="assets/next-step-indicator.png" />
        </div>
        <div class="steps-container">
            <div class="instruction"></div>
            <p class="step-count">2</p>
            <p class="step-title">Documents</p>
            <div class="step-description-container">
                <p class="step-description">Upload required documents</p>
            </div>
            <div class="step-container-button">
                <asp:Button ID="btnUploadDocumentButton" runat="server" Text="UPLOAD DOCUMENT" CssClass="btnRegistrationButton" />
            </div>
        </div>
        <div class="next-step-indicator">
            <img src="assets/next-step-indicator.png" />
        </div>
        <div class="steps-container">
            <div class="instruction"></div>
            <p class="step-count">3</p>
            <p class="step-title">Verification</p>
            <div class="step-description-container">
                <p class="step-description">Wait for verification</p>
            </div>
            <div class="step-container-button">
                <asp:Button ID="btnViewStatusButton" runat="server" Text="VIEW STATUS" CssClass="btnRegistrationButton" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="FirstRegistrationForm" ContentPlaceHolderID="FirstRegistrationForm" runat="server">
    <div class="page-indicator">
        <div class="page-text">
            <img src="assets/page-one.png" />
            <p>Personal Information</p>
        </div>
        <div class="triangle-right"></div>
        <div class="page-text">
            <img src="assets/page-two.png" />
            <p>Oath and Consent</p>
        </div>
        <div class="triangle-right"></div>
    </div>
    <div id="first-registration-form" class="page-container" style="background-color: #FAF7EE;">
        <div class="form-divider" style="width: 630px;">
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">NAME</p>
            <div style="display: flex; align-items: center; justify-content: space-between;">
                <div style="display: flex; flex-direction: column;">
                    <div style="margin: 5px 0 5px 0px; display: flex;">
                        <p style="color: #736963;">First</p>
                        <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 325px; margin: 0 0 0 20px;"></asp:TextBox>
                    </div>
                    <div style="margin: 5px 0 5px 0px; display: flex;">
                        <p style="color: #736963;">Last</p>
                        <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 325px; margin: 0 0 0 20px;"></asp:TextBox>
                    </div>
                </div>
                <div style="display: flex; flex-direction: column;">
                    <div style="margin: 5px 0 5px 0px; display: flex;">
                        <p style="color: #736963;">Middle</p>
                        <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 140px; margin: 0 0 0 20px;"></asp:TextBox>
                    </div>
                    <div style="margin: 5px 0 5px 0px; display: flex;">
                        <p style="color: #736963;">Suffix</p>
                        <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 140px; margin: 0 0 0 35px;"></asp:TextBox>
                    </div>
                </div>
            </div>
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">RESIDENCE/ADDRESS</p>
            <div style="margin: 5px 0 5px 0px; display: flex; flex-direction: column;">
                <p style="color: #736963;">House No./ Street</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField"></asp:TextBox>
            </div>
            <div style="margin: 5px 0 5px 0px; display: flex; justify-content: space-between;">
                <div>
                    <p style="color: #736963;">Baranggay/Sitio/Purok</p>
                    <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 300px;"></asp:TextBox>
                </div>
                <div>
                    <p style="color: #736963;">City/Municipality</p>
                    <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 300px;"></asp:TextBox>
                </div>
            </div>
            <div style="margin: 5px 0 5px 0px; display: flex; flex-direction: column;">
                <p style="color: #736963;">Province</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField"></asp:TextBox>
            </div>
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">CITIZENSHIP</p>
            <div style="margin: 5px 0 5px 0px;">
                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                    <asp:ListItem Text="By Birth" Value="By Birth" Style="color: #736963;" />
                    <asp:ListItem Text="Naturalized" Value="Naturalized" Style="color: #736963;" />
                    <asp:ListItem Text="Reacquired" Value="Reacquired" Style="color: #736963;" />
                </asp:RadioButtonList>
            </div>
            <p style="margin: 5px 0 5px 0px; font-size: 15px; text-align: center; color: #736963;">(If naturalized/reacquired, state date of naturalization/reacquisition and Certificate Number of naturalization/order of approval of reacquisition)</p>
            <div style="margin: 5px 0 5px 0px; display: flex; align-items: center; justify-content: space-between; color: #736963;">
                <div style="display: flex; align-items: center;">
                    <p style="font-size: 15px; color: #736963;">Date of Naturalization/<br>Reacquisition</p>
                    <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 135px; margin: 0 0 0 20px;"></asp:TextBox>
                </div>
                <div style="display: flex; align-items: center;">
                    <p style="font-size: 15px; color: #736963;">Certificate No./<br>Order of Approval</p>
                    <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 135px; margin: 0 0 0 20px;"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-divider" style="width: 475px;">
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">SEX</p>
            <div style="margin: 5px 0 5px 0px;">
                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                    <asp:ListItem Text="Male" Value="Male" Style="color: #736963;" />
                    <asp:ListItem Text="Female" Value="Female" Style="color: #736963;" />
                </asp:RadioButtonList>
            </div>
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">DATE OF BIRTH</p>
            <div style="margin: 5px 0 5px 0px; display: flex; flex-direction: column;">
                <asp:TextBox runat="server" CssClass="txtRegistrationField"></asp:TextBox>
            </div>
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">PLACE OF BIRTH</p>
            <div style="margin: 5px 0 5px 0px; display: flex; flex-direction: column;">
                <p style="color: #736963;">City/Municipality</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField"></asp:TextBox>
            </div>
            <div style="margin: 5px 0 5px 0px; display: flex; flex-direction: column;">
                <p style="color: #736963;">Province</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField"></asp:TextBox>
            </div>
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">PARENT'S NAME</p>
            <div style="margin: 5px 0 5px 0px; display: flex; justify-content: space-between">
                <p style="color: #736963;">Father's Name</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 300px;"></asp:TextBox>
            </div>
            <div style="margin: 5px 0 5px 0px; display: flex; justify-content: space-between">
                <p style="color: #736963;">Mother's Name</p>
                <asp:TextBox runat="server" CssClass="txtRegistrationField" Style="width: 300px;"></asp:TextBox>
            </div>
            <div>
                <asp:Button CssClass="btnNextPageButton" runat="server" Text="NEXT"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="SecondRegistrationForm" ContentPlaceHolderID="SecondRegistrationForm" runat="server">
    <div class="page-indicator">
        <div class="page-text">
            <img src="assets/page-one.png" />
            <p>Personal Information</p>
        </div>
        <div class="triangle-right"></div>
        <div class="page-text">
            <img src="assets/page-two.png" />
            <p>Oath and Consent</p>
        </div>
        <div class="triangle-right"></div>
    </div>
    <div id="second-registration-form" class="page-container" style="height: calc(100vh - 174px); background-color: #FAF7EE;">
        <div class="form-divider" style="width: 1155px; display: flex; flex-direction: column; align-items: center;">
            <p style="margin: 5px 0 5px 0px; font-weight: 700; color: #294278;">OATH, NOTICE, and CONSENT (REGULAR)</p>
            <div style="margin: 5px 0 5px 0px;">
                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                    <asp:ListItem Text="REGISTRATION" Value="Registration" Style="font-weight: 700; color: #294278;" />
                    <asp:ListItem Text="TRANSFER" Value="Transfer" Style="font-weight: 700; color: #294278;" />
                </asp:RadioButtonList>
            </div>
            <p style="margin: 5px 0 5px 0px; text-align:justify; color: #736963;">I do solemnly swear that the above statements regarding my person are true and correct; that I possess all the qualifications and none of the disqualifications of a voter; and that I am:</p>
            <div style="margin: 5px 0 5px 0px;">
                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                    <asp:ListItem Text="not registered at the<br>precinct" Value="notRegisteredInPrecinct" Style="text-align: center; color: #736963;" />
                    <asp:ListItem Text="registered in a precinct of another City/<br>Municipality/District in the Philippines" Value="registeredInPrecinct" Style="text-align: center; color: #736963;" />
                </asp:RadioButtonList>
            </div>
            <p style="margin: 5px 0 5px 0px; text-align:justify; color: #736963;">and that I have reviewed the entries encoded in the VRS and I  confirm that the same are correct, accurate and consistent with  the information I supplied in this application form. Further, by  affixing my signature below, I authorize and give my consent to  the Commission on Elections and the concerned Election  Registration Board to collect and process the personal data I  supplied herein for purposes of voter registration and  elections, and for other purposes and allowable disclosures  under B.P. Blg. 881, R.A. No. 8189, 10173 and 10367, and the  relevant Resolutions of the Commission on Elections.</p>
        </div>
        <div>
            <asp:Button runat="server" Text="SUBMIT" CssClass="btnSubmitButton"></asp:Button>
        </div>
    </div>
</asp:Content>

<asp:Content ID="UploadDocumentPage" ContentPlaceHolderID="UploadDocumentPage" runat="server">
    <div id="upload-document-page" class="page-container" style="height: calc(100vh - 100px); background-color: #FAF7EE; display: flex; align-items: center; justify-content: center;">
        <div class="form-divider" style="width: 50%; box-sizing: border-box; margin: 0 25px 0 25px; padding: 20px; display: flex; flex-direction: column; align-items: center; justify-content: center;">
            <p style="margin: 5px 0 5px 0; text-align: center; font-size: 35px; font-weight: 700; color: #294278;">UPLOAD 1 VALID IDENTIFICATION DOCUMENT</p>
            <p style="margin: 5px 0 5px 0; text-align: justify; font-size: 15px; color: #736963;">Please rename the file by incorporating your last name followed by your first name, along with the number of your valid document. The number can be found on the right panel.</p>
            <p style="margin: 5px 0 5px 0; align-self: flex-start; font-size: 15px; font-weight: 700; color: #E5A73A;">Example: DelaCruz_Juan_1.jpeg</p>
            <div style="width: 100%; box-sizing: border-box; margin: 5px 0 5px 0;  padding: 10px; background-color: #FFFFFF; border: 2px dashed #000000; border-radius: 50px; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <img src="assets/upload-icon.png" style="width: 250px; height: 250px;" />
                <button id="pseudoFileUploadButton" onclick="clickFileUpload()">CHOOSE FILE</button>
                <asp:FileUpload ID="fileUploadControl" runat="server" ClientIDMode="Static" CssClass="fileUpload" />
            </div>
            <asp:Button runat="server" Text="SUBMIT" CssClass="btnSubmitButton" Style="border-radius: 25px;"></asp:Button>
        </div>
        <div class="form-divider" style="width: 50%; box-sizing: border-box; margin: 0 25px 0 25px; padding: 20px; background-color: #FFFFFF; border: 2px solid #000000; border-radius: 25px; display: flex; flex-direction: column; font-size: 15px;">
            <p style="margin: 0 0 5px 0; font-size: 35px; font-weight: 700; color: #294278;">DOCUMENTARY REQUIREMENTS</p>
            <p style="margin: 5px 0 5px 0; color: #E5A73A;">A person has to establish his (or her) identity upon filing his (or her) application. The following are valid identification documents:</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">1. National Identification (ID) card under the Philippine Identification System (PhilSys)</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">2. Postal ID Card</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">3. PWD ID Card</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">4. Student's ID Card or Library Card, signed by the School Authority</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">5. SC's ID card</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">6. Land Transportation Office (LTO) Driver's License/Student Permit</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">7. National Bureau of Investigation (NBI) Clearance</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">8. Philippine Passport</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">9. Social Security System (SSS)/Government Service Insurance System (GSIS) or other Unified Multi-Purpose ID card</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">10. Integrated Bar of the Philippines (IBP) ID Card</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">11. License issued by the Professional Regulatory Commission (PRC)</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">12. Certificate of Confirmation issued by the National Commission on Indigenous People (NCIP) in case of members of ICCs or Ips</p>
            <p style="margin: 5px 0 5px 0; color: #736963;">13. Barangay Identification / Certification with photo</p>
            <p style="margin: 5px 0 0 0; color: #736963;">14. Any other Government issued valid IDs</p>
        </div>
    </div>
</asp:Content>

<asp:Content ID="NewsAndEventsPage" ContentPlaceHolderID="NewsAndEventsPage" runat="server">
    <div id="news-page" class="page-container" style="background-color: #FAF7EE;">
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
    <div id="about-page" class="page-container" style="background-color: #FAF7EE;">
        <div class="about-section">
            <p class="section-title">I. Overview of E-Rehistro</p>
            <ul class="accordion">
                <li>
                    <input type="radio" name="accordion" id="first">
                    <label for="first">What is E-Rehistro?</label>
                    <div class="content">
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Suscipit eius corporis blanditiis eaque, consequatur ab iusto voluptate soluta aspernatur enim ipsam. Architecto, atque suscipit? Consectetur unde earum molestiae quis in.</p>
                    </div>
                </li>
                <li>
                    <input type="radio" name="accordion" id="second">
                    <label for="second">How long is the validity of E-Rehistro?</label>
                    <div class="content">
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Ratione officiis recusandae voluptates mollitia explicabo odit soluta consequuntur ipsam nulla? Quia distinctio ex, expedita quos quod natus saepe provident magni accusantium.</p>
                    </div>
                </li>
            </ul>
        </div>
        <div class="about-section">
            <p class="section-title">II. Registration Process</p>
            <ul class="accordion">
                <li>
                    <input type="radio" name="accordion" id="third">
                    <label for="third">What are the steps in registering in E-Rehistro?</label>
                    <div class="content">
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Suscipit eius corporis blanditiis eaque, consequatur ab iusto voluptate soluta aspernatur enim ipsam. Architecto, atque suscipit? Consectetur unde earum molestiae quis in.</p>
                    </div>
                </li>
                <li>
                    <input type="radio" name="accordion" id="fourth">
                    <label for="fourth">How long  is the Registration Process?</label>
                    <div class="content">
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Ratione officiis recusandae voluptates mollitia explicabo odit soluta consequuntur ipsam nulla? Quia distinctio ex, expedita quos quod natus saepe provident magni accusantium.</p>
                    </div>
                </li>
            </ul>
        </div>
        <div class="about-section">
            <p class="section-title">III. Data Privacy and Security</p>
            <ul class="accordion">
                <li>
                    <input type="radio" name="accordion" id="fifth">
                    <label for="fifth">How will E-Rehistro ensure the data privacy and security of the informations being uploaded?</label>
                    <div class="content">
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Ratione officiis recusandae voluptates mollitia explicabo odit soluta consequuntur ipsam nulla? Quia distinctio ex, expedita quos quod natus saepe provident magni accusantium.</p>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContactsPage" ContentPlaceHolderID="ContactsPage" runat="server">
    <div id="contacts-page" class="page-container" style="background-color: #FAF7EE;">
    </div>
</asp:Content>
