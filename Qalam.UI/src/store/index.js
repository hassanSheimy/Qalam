import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        countries: [],
        user: {
            upToDate: false,
            id: 0,
            userName: "",
            firstName: "",
            lastName: "",
            phoneNumber: "",
            birthDate: "",
            imagePath: null,
            about: "",
            streamKey: "",
            facebook: "",
            linkedIn: "",
            countryId: 0,
            educationTypeId: 0,
            educationYearId: 0,
            subjectName: "",
            notifications: []
        },
        languages: []
    },
    getters: {
        Countries(state) {
            return state.countries;
        },
        Languages(state) {
            return state.languages;
        },
        UserDetails(state){
            return state.user;
        }
    },
    mutations: {
        InitCountries(state, countries){
            state.countries = countries;
        },
        AddCountry(state, country){
            state.countries.push(country);
        },
        UpdateCountry(state, country){
            state.countries[country.id] = country;
        },
        InitLanguages(state, languages){
            state.languages = languages;
        },
        UpdateUserData(state, user){
            state.user = Object.assign(state.user, user);
        }
    },
    actions: {
    },
    modules: {
    }
})
