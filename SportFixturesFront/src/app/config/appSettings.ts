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
        addTeam: `${AppSettings.BaseUrl}teams`,
        updateTeam: `${AppSettings.BaseUrl}teams`,
        deleteTeam: `${AppSettings.BaseUrl}teams/`,
        getFavorites: `${AppSettings.BaseUrl}users/favorites/`,
        getEncountersOfTeam: `${AppSettings.BaseUrl}encounters/team/`
    };

    public static readonly RouterUrls: any = {
        home: "home",
        login: "login",
        sportsManagement: "sportsManagement",
        teamsManagement: "teamsManagement"
    };
}