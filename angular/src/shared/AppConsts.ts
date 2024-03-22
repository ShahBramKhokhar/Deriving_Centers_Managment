export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish
    static defaultPassword: '123qwe';
    static localeMappings: any = [];
    static defaultImageUrl = 'assets/img/companyLogo.png';
    static maxImageSize = 2000000;
    static allowedImageTypes = ['image/png', 'image/jpeg'];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'ManagementSystem'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };
}
