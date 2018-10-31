export class AppSettings {
    public static readonly BaseUrl: string = "http://localhost:50144/api/";
    public static readonly localstorageToken: string = "userToken";

    public static readonly ApiEndpoints: any = {
        login: `${AppSettings.BaseUrl}login`,
        logout: `${AppSettings.BaseUrl}logout`,
        getAllUsers: `${AppSettings.BaseUrl}users`,
        addSport: `${AppSettings.BaseUrl}sports`,
        getSports: `${AppSettings.BaseUrl}sports`,
        updateSport: `${AppSettings.BaseUrl}sports`,
        deleteSport: `${AppSettings.BaseUrl}sports/`,
    };

    public static readonly RouterUrls: any = {
        login: "login",
        sportsManagement: "sportsManagement"
    };
}