using System.ComponentModel;

namespace WebApiTestClient.Models
{
    public enum CancellationReasons : int
    {


        [Description("Gone to a competitor")]
        GoneToACompetitor = 1,
        [Description("Failed to pay")]
        FailedToPay = 2,
        [Description("Taken in house")]
        TakenInHouse = 3,
        [Description("Mis-sold")]
        MisSold = 4,
        [Description("Offer ended")]
        OfferEnded = 5,
        [Description("Problem with customer website")]
        ProblemWithCustomersWebsite = 6,
        [Description("Poor Performance")]
        PoorPerformance = 7,
        [Description("Cancelled before starting service")]
        CancelledBeforeStartingService = 8,
        [Description("Other")]
        Other = 9,
        [Description("Our Fees")]
        Price = 10,
        [Description("Contract length")]
        ContractLength = 11,
        [Description("Competitor")]
        Competitor = 12,
        [Description("Service deliverables")]
        ServiceDeliverables = 13,
        [Description("Location")]
        Location = 14,
        [Description("Customer feels mis-sold")]
        CustomerFeelsMisSold = 15,
        [Description("Billing issue")]
        BillingIssue = 16,
        [Description("Initial complaint mis-handled")]
        InitialComplaintMishandled = 17,
        [Description("Poor customer contact")]
        PoorCustomerContact = 18,
        [Description("Hosting")]
        Hosting = 19,
        [Description("Design issue")]
        DesignIssue = 20,
        [Description("Metered number issues")]
        MeteredNumberIssues = 21,
        [Description("Functionality issue")]
        FunctionalityIssue = 22,
        [Description("Buyers response")]
        BuyersResponse = 23,
        [Description("Poor roi")]
        PoorRoi = 24,
        [Description("Affordability expense")]
        AffordabilityExpense = 25,
        [Description("Competitor")]
        CancelCompetitor = 26,
        [Description("In life service issue")]
        InlifeServiceIssue = 27,
        [Description("Non a cancellation job")]
        NonACancellationJob = 28,
        [Description("Changing products")]
        ChangingProducts = 29,
        [Description("Unable to contact")]
        UnableToContact = 30,
        [Description("Budget")]
        Budget = 31,
        [Description("On Hold")]
        OnHold = 32,
    }
}