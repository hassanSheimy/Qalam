<template>
    <v-container>
        <v-row>
            <v-col cols="12">
                <v-btn color="primary" @click="OpenSaveModal">{{$locales.t('addNewCountryBtn')}}</v-btn>
            </v-col>

            <v-col cols="12">
                <empty-state v-if="countries.length == 0" />
            </v-col>

            <card-with-icons md="6" cols="12" can-delete detailsBtn
            :btnText="$locales.t('detailsBtn')"
            v-for="(country, index) in countries" 
            :key="'country-' + index" :data="country" 
            :icons="icons" :title="country.name"
            :btnPath="`/profile/admin/country/${country.id}`" 
            @editClicked="EditClicked"
            @deleteClicked="DeleteClicked" />

        </v-row>

        <!-- Modals -->
        <save-country-modal :dialog="saveDialog" @close="SaveModalClosed" @done="SaveModalDone" :old-country="selectedCountry" />
        <delete-country-modal :dialog="deleteDialog" :id="selectedCountry" @close="DeleteModalClosed" @done="DeleteModalDone" />
        
    </v-container>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import CardWithIcons from '@/components/cards/card-with-icons.vue'
import SaveCountryModal from '@/components/modals/save-country-modal.vue'
import DeleteCountryModal from '@/components/modals/delete-country-modal.vue'

export default {
    name: "AdminGeneralSettings",

    components: {
        EmptyState,
        CardWithIcons,
        SaveCountryModal,
        DeleteCountryModal
    },

    data: () => ({
        saveDialog: false,
        deleteDialog: false,
        selectedCountry: {},
        selectedIndex: -1,
        isEdit: false,
        icons: [
            {icon: 'fas fa-school', value: 'educationTypesCount'}, 
            {icon: 'fas fa-graduation-cap', value: 'educationYearsCount'}, 
            {icon: 'fas fa-book', value: 'subjectsCount'},
        ],
        countries: []
    }),

    created() {
        this.GetCountries();
    },

    methods: {
        GetCountries(){
            this.HttpRequest({
                url: 'api/countries',
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            response.data.forEach(c => {
                c.educationTypesCount = `${this.$locales.t('educationTypesCount')} ${c.educationTypesCount}`;
                c.educationYearsCount = `${this.$locales.t('educationYearsCount')} ${c.educationYearsCount}`;
                c.subjectsCount = `${this.$locales.t('subjectsCount')} ${c.subjectsCount}`
            });
            this.countries = response.data;
        },
        GetFailed(error){
            // error happened
        },
        OpenSaveModal(){
            this.isEdit = false;
            this.selectedCountry = null;
            this.saveDialog = true;
            this.$eventBus.$emit("InitForm");
        },
        EditClicked(id){
            this.isEdit = true
            this.selectedIndex = this.countries.findIndex(c => c.id == id);
            this.selectedCountry = this.countries.find(c => c.id == id);
            this.saveDialog = true;
            this.$eventBus.$emit("InitForm", this.selectedCountry);
        },
        DeleteClicked(id){
            this.selectedCountry = id;
            this.selectedIndex = this.countries.findIndex(c => c.id == id);
            this.deleteDialog = true;
        },
        SaveModalDone(country){
            if(this.isEdit){
                this.countries[this.selectedIndex] = country;
            }
            else{
                this.countries.push(country);
            }
            this.saveDialog = false;
        },
        SaveModalClosed(){
            this.saveDialog = false;
        },
        DeleteModalDone(){
            this.countries.splice(this.selectedIndex, 1);
            this.deleteDialog = false;
        },
        DeleteModalClosed(){
            this.deleteDialog = false;
        }
    }
}
</script>

<style scoped>

</style>