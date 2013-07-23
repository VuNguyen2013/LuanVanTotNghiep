namespace ETradeAutomation
{
    class Constants
    {
        public const string ACTIVATION_TASK = "ETrade Automation Activation";
        public const string CONDITION_ORDER_TASK = "ETrade Automation Condition Order";
        public const string CURRENCIES_TASK = "ETrade Currencies Updater";
        public const string CASH_ADVANCE_CLEAN_UP_TASK = "Clean up expired ETrade cash advance";
        public const string RESET_CONDITION_ORDER_TABLE_TASK = "Reset Condition Order Table";
        public const string UPDATE_COMPANY_INFO_TASK = "Update company info";

        public const string EXECUTION_FILENAME = "ETradeAutomation.exe";
        public const string ACTIVATION_TASK_FILE = "ActivateService.bat";
        public const string CONDITION_ORDER_TASK_FILE = "PutConditionOrders.bat";
        public const string CURRENCIES_TASK_FILE = "UpdateCurrencies.bat";
        public const string CASH_ADVANCE_CLEAN_UP_TASK_FILE = "CleanUpExpiredCashAdvance.bat";
        public const string RESET_CONDITION_ORDER_TABLE_TASK_FILE = "ResetConditionOrderTable.bat";
        public const string UPDATE_COMPANYINFO = "UpdateCompanyInfo.bat";

        public enum TaskId
        {
            ACTIVATION = 1,
            CONDITION_ORDER,
            CURRENCIES,
            CASH_ADVANCE_CLEAN_UP,
            RESET_CONDITION_ORDER,
            UPDATE_COMPANYINFO
        }
    }
}
