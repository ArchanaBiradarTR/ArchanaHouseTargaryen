namespace WlnIndigoRegression.Tests.Features.TaxLiveChat
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums.Footer;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Footer;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Framework.Core.QualityChecks.MsUnit;
    using Framework.Core.QualityChecks.Result;

    using WlnIndigoRegression.Tests.Features.WLNTax;
    using Framework.Core.Utils.Verification;
    using OpenQA.Selenium.Chrome;

    [TestClass]
    public sealed class LiveChatWidgetTests : BaseTaxTests
    {
        /// <summary>
        /// Business Story 1010948 - WLN Tax Migration - Create CHAT Link with CPET
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Agent Offline text is displayed
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat")]
        [TestProperty("IRS", "Apply")]
        public void LiveChatWidgetIrsPageTest()
        {
            var homePage = this.GetHomePage<EdgeHomePage>();
            var irsPage = homePage.BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes).ClickBrowseCategory<CommonMigratedTaxPage>(ContentTypeEdge.IrsTax);
            this.LiveChatWidgetTest(irsPage);
        }

        /// <summary>
        /// Business Story 1010948 - WLN Tax Migration - Create CHAT Link with CPET
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Agent Offline text is displayed
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat")]
        [TestProperty("PWC", "Apply")]
        public void LiveChatWidgetPwcPageTest()
        {
            this.LiveChatWidgetTest();
        }

        /// <summary>
        /// Business Story 1010948 - WLN Tax Migration - Create CHAT Link with CPET
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Agent Offline text is displayed
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat")]
        [TestProperty("KPMG", "Apply")]
        public void LiveChatWidgetKpmgPageTest()
        {
            this.LiveChatWidgetTest();
        }

        /// <summary>
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Live chat off hours message is displayed.
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat_OffHours")]
        [TestProperty("IRS", "Apply")]
        public void LiveChatOffHoursWidgetIrsPageTest()
        {
            var homePage = this.GetHomePage<EdgeHomePage>();
            var irsPage = homePage.BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes).ClickBrowseCategory<CommonMigratedTaxPage>(ContentTypeEdge.IrsTax);
            this.LiveChatOffHoursWidgetTest(irsPage);
        }

        /// <summary>
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Live chat off hours message is displayed.
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat_OffHours")]
        [TestProperty("PWC", "Apply")]
        public void LiveChatOffHoursWidgetPwcPageTest()
        {
            this.LiveChatOffHoursWidgetTest();
        }

        /// <summary>
        /// 1. Sign-on to WLN.
        /// * Verify that the 'Live Chat' link is present in footer.
        /// * Verify that the 'Live Chat' link is present in IRS Resources widget. 
        /// 2. Click the "Live Chat" link.
        /// * Verify that Live chat off hours message is displayed.
        /// 3. Close the Live chat box
        /// * Verify that Session paused dialog is displayed
        /// </summary>
        [TestMethod]
        [TestCategory("IndigoRegression")]
        [TestCategory("WLNTaxLiveChat_OffHours")]
        [TestProperty("KPMG", "Apply")]
        public void LiveChatOffHoursWidgetKpmgPageTest()
        {
            this.LiveChatOffHoursWidgetTest();
        }

        /// <summary>
        /// Settings
        /// </summary>
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.LiveChatCommercial);
        }

        /// <summary>
        /// Live chat widget test.
        /// eScrum Product Backlog Item 1344644:Live Chat : Commented few lines of code related to send button as this functionality is not yet implemented
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        private void LiveChatWidgetTest(CommonMigratedTaxPage page = null)
        {
            const string AgentOfflineText = "Hello, have a question? Let’s chat.";
            const string LiveChatTabName = "Chat by SalesForce";
            const string LiveChatLinkText = "Live chat";

            var agentOfflineDisplayCheck = new QualityCheck("Verify that Agent Offline text is displayed.");
            var liveChatLinkInFooterCheck = new QualityCheck("Live chat link is displayed in footer.");
            var liveChatLinkInWidgetCheck = new QualityCheck("Live chat link is displayed in Resources widget.");
            var sessionPauseCheck = new QualityCheck("Verify that 'Session Paused' message displayed.");

            this.QualityTestCase.AddQualityChecks(agentOfflineDisplayCheck, liveChatLinkInFooterCheck, liveChatLinkInWidgetCheck, sessionPauseCheck);

            CommonMigratedTaxPage homePage = page ?? this.GetHomePage<CommonMigratedTaxPage>();

            QualityVerify.IsTrue(liveChatLinkInFooterCheck, homePage.Footer.IsFooterLinkDiplayed(FooterLinks.LiveChat));
            QualityVerify.IsTrue(liveChatLinkInWidgetCheck, homePage.Resources.IsLiveChatLinkInSupportResourcesComponentDisplayed());

            var liveChatStreamPage = homePage.Footer.ClickLinkAndOpenNewBrowserTab<LiveChatStreamPage>(LiveChatLinkText, LiveChatTabName);
            QualityVerify.AreEqual(agentOfflineDisplayCheck, AgentOfflineText, liveChatStreamPage.AgentOfflineLabel.Text);

            var sessionPausedDialog = homePage.CloseCurrentAndSwitchToDefaultTab<SessionPauseDialog>(LiveChatTabName);

            QualityVerify.IsTrue(sessionPauseCheck, sessionPausedDialog.WaitForSessionPausedDialog());
        }

        /// <summary>
        /// Live chat off hours widget test.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        private void LiveChatOffHoursWidgetTest(CommonMigratedTaxPage page = null)
        {
            const string LiveChatTabName = "Chat by SalesForce";
            const string LiveChatLinkText = "Live chat";
            const string LiveChatOffHoursText = "Live Help is available from 7 a.m.- 10 p.m. (Central Time), Monday through Friday.";

            var liveChatLinkInFooterCheck = new QualityCheck("Live chat link is displayed in footer.");
            var liveChatLinkInWidgetCheck = new QualityCheck("Live chat link is displayed in Resources widget.");
            var liveChatOffHoursTextDisplayCheck = new QualityCheck("Verify that Live chat off hours text is displayed.");
            var sessionPauseCheck = new QualityCheck("Verify that 'Session Paused' message displayed.");

            this.QualityTestCase.AddQualityChecks(liveChatLinkInFooterCheck, liveChatLinkInWidgetCheck, liveChatOffHoursTextDisplayCheck, sessionPauseCheck);

            CommonMigratedTaxPage homePage = page ?? this.GetHomePage<CommonMigratedTaxPage>();

            QualityVerify.IsTrue(liveChatLinkInFooterCheck, homePage.Footer.IsFooterLinkDiplayed(FooterLinks.LiveChat));
            QualityVerify.IsTrue(liveChatLinkInWidgetCheck, homePage.Resources.IsLiveChatLinkInSupportResourcesComponentDisplayed());

            var liveChatStreamPage = homePage.Footer.ClickLinkAndOpenNewBrowserTab<LiveChatStreamPage>(LiveChatLinkText, LiveChatTabName);
            QualityVerify.IsTrue(liveChatOffHoursTextDisplayCheck, liveChatStreamPage.LiveChatOffHoursLabel.Text.Contains(LiveChatOffHoursText));

            var sessionPausedDialog = homePage.CloseCurrentAndSwitchToDefaultTab<SessionPauseDialog>(LiveChatTabName);

            QualityVerify.IsTrue(sessionPauseCheck, sessionPausedDialog.WaitForSessionPausedDialog());
        }
        /// <summary>
        /// Retrieves Google Chrome browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">
        /// The path to the browser executable file.
        /// </param>
        /// <returns>
        /// Google Chrome browser options
        /// </returns>
        protected override sealed ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = new ChromeOptions();

            browserOptions.AddArguments(
                "--start-maximized",
                "--allow-running-insecure-content",
                "--disable-gpu");
            browserOptions.AddExcludedArguments("test-type", "ignore-certificate-errors");
                        
            if (!string.IsNullOrEmpty(pathToBrowserExecutable))
            {
                Assertion.FileExists(pathToBrowserExecutable);
                browserOptions.BinaryLocation = pathToBrowserExecutable;
            }

            return browserOptions;
        }

    }
}
