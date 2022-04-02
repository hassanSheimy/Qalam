import '@fortawesome/fontawesome-free/css/all.css'
import Vue from 'vue';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify)

export default new Vuetify({
    rtl: true,
    theme: {
        dark: false,
        themes: {
            light: {
                primary: '#79a05f',
                secondary: '#212d31',
                sidebar: '212d31',
                header: '#fff',
                background: '#f2f2f2',
                error: '#b71c1c',
                info: '#2196F3',
                success: '#4CAF50',
                warning: '#FFC107',
                systemBar: '#79a05f'
            },
            dark: {
                primary: '#79a05f',
                secondary: '#fff',
                sidebar: '#1e1e1e',
                header: '#272727',
                background: '#272727',
                systemBar: '#1b1a1a'
            }
        }
    },
    breakpoint: {
        thresholds: {
            xs: 576 + 24,  // add 24 for scroll-bar width
            sm: 768 + 24,
            md: 992 + 24,
            lg: 1200 + 24,
        },
        scrollBarWidth: 24,
    },
    icons: {
        iconfont: 'fa',
    }
});
