export class AppSettings {
    public static readonly BaseUrl: string = "http://localhost:50144/api/";
    public static readonly localstorageToken: string = "userToken";

    public static readonly ApiEndpoints: any = {
        login: `${AppSettings.BaseUrl}login`,
        logout: `${AppSettings.BaseUrl}logout`,
        getAllUsers: `${AppSettings.BaseUrl}users`,
    };

    public static readonly RouterUrls: any = {
        login: "login",
        sportsManagement: "sportsManagement"
    };
}