import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import helper from './mixins/helper'
import chartist from 'vue-chartist'
import SignlR from './plugins/signalr'
import locales from './locales'
import Notifications from 'vue-notification'

// styles
import "@/assets/styles/main.css"
import 'video.js/dist/video-js.css';
import 'chartist/dist/chartist.min.css'

Vue.config.productionTip = false

const eventBus = new Vue();

// Array exstension
Array.prototype.removeIf = function(callback) {
    var i = this.length;
    while (i--) {
        if (callback(this[i], i)) {
            this.splice(i, 1);
        }
    }
};

// gloabal variables
Vue.prototype.$locales = locales;
Vue.prototype.$eventBus = eventBus;
if(process.env.NODE_ENV === 'development'){
    Vue.prototype.$BACKEND_APP_URL = 'https://localhost:44366'
} else {
    Vue.prototype.$BACKEND_APP_URL = 'https://qlam.azurewebsites.net';
}
Vue.prototype.$LIVE_APP_URL = 'http://127.0.0.1:8888';

Vue.mixin(helper);
Vue.use(chartist);
Vue.use(SignlR);
Vue.use(Notifications);

console.log(process.env.NODE_ENV)

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app')
