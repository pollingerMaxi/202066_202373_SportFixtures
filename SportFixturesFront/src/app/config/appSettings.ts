export class AppSettings {
    public static readonly BaseUrl: string = "http://localhost:50144/api/";
    public static readonly localstorageToken: string = "userToken";
    public static readonly localstorageUser: string = "user";

    public static readonly ApiEndpoints: any = {
        login: `${AppSettings.BaseUrl}login`,
        logout: `${AppSettings.BaseUrl}logout`,
        getAllUsers: `${AppSettings.BaseUrl}users`,
        addSport: `${AppSettings.BaseUrl}sports`,
        getSports: `${AppSettings.BaseUrl}sports`,
        updateSport: `${AppSettings.BaseUrl}sports`,
        deleteSport: `${AppSettings.BaseUrl}sports/`,
        getTeams: `${AppSettings.BaseUrl}teams`,
        getSportById: `${AppSettings.BaseUrl}sports/`,
        getLogs: `${AppSettings.BaseUrl}logger?from=18112018&to=18112019`
    };

    public static readonly RouterUrls: any = {
        login: "login",
        sportsManagement: "sportsManagement",
        teamsManagement: "teamsManagement",
        actionsReport: "actionsReport"
    };
}