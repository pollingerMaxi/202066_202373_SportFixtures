export class AppSettings {
    public static readonly BaseUrl: string = "http://localhost:50144/api/";

    public static readonly ApiEndpoints: any = {
        login: `${AppSettings.BaseUrl}login`
    };

    public static readonly RouterUrls: any = {
        login: "login",
    };
}