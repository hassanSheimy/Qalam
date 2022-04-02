import ar from './ar.js'
import en from './en.js'

var locales = {
    current: 'ar',
    ar: ar,
    en: en,

    /* t alias for translate */
    t: function(key) {
        var text = (key.split('.')).reduce(((obj, key)=> obj[key]), this[this.current]);
        return text;
    },

    translate: function(key) {
        this.t(key);
    },

    isRTL: function() {
        return this[this.current].rtl;
    },

    setLocale: function(lang) {
        this.current = lang;
    }
}

export default locales;