namespace StarmanLibrary.Services
{
    public interface ICommunicationService
    {
        string GetHelloMessage();

        string GetMarsStatus();

        string GetMoonStatus();

        string GetIssStatusText();

        double[] GetIssPosition();

        string GetHumansInSpace();

        string GetSpacePics();

        string[] GetPicOfTheDayInfo();

        string[] GetPicOfTheEarthInfo();

        double[] GetLocationOfTheEarth();

        string GetDefaultResponse();

        string GetHomelandResponse();

        string GetBackResponse();

        string GetHelp();

        string GetSettings();

        string GetSpacexCompanyInfo();

        string GetSpacexRocketsInfo();

        string GetLaunchesInfo();
    }
}
