import Oidc from 'oidc-client';

var mgr = new Oidc.UserManager({
    authority: 'https://localhost:44335',
    client_id: 'augustinesenatorspa',
    redirect_uri: 'http://localhost:8080/callback',
    response_type: 'id_token token',
    scope: 'openid profile senatorsapi',
    post_logout_redirect_uri: 'http://localhost:8080',
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
})

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.DEBUG;

export default mgr;