import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);
import app from './modules/app'
import session from './modules/session'
import account from './modules/account'
import user from './modules/user'
import role from './modules/role'
import tenant from './modules/tenant'
import dictionary from './modules/dictionary'
import dictionarydetail from './modules/dictionarydetail'
import sysmodule from './modules/sysmodule'
import action from './modules/action'
import moduleactioninrole from './modules/moduleactioninrole'
const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        session,
        account,
        user,
        role,
        tenant,
        dictionary,
        dictionarydetail,
        sysmodule,
        action,
        moduleactioninrole
    }
});

export default store;