<template>
    <v-form class="justify-center" ref="form" :lazy-validation="true" >
        <v-container fluid>
            <v-row no-gutters>
                <v-col cols="12" md="6" sm="8">
                    <v-text-field class="mt-2" outlined
                    v-model="country.name" :rules="validationRules.name"
                    counter="32" :label="$locales.t('country')"
                    prepend-icon="fas fa-globe-africa" required />
                </v-col>
                <v-col cols="11" class="mb-4">
                    <v-divider />
                </v-col>
            </v-row>
            <v-row no-gutters>
                <v-col cols="12" sm="8" align-self="center">
                    <v-text-field class="mt-2"
                    outlined v-model="educationType.name"
                    :rules="validationRules.name" counter="32"
                    :label="$locales.t('educationType')"
                    prepend-icon="fas fa-school" required />
                </v-col>
                <v-col cols="6" sm="1" align-self="center">
                    <v-btn outlined @click="AddChip" color="primary" class="ma-4">
                        <v-icon left color="primary" size="16">fas fa-plus-circle</v-icon>
                        {{$locales.t('addBtn')}}
                    </v-btn>
                </v-col>
            </v-row>
            <v-row no-gutters>
                <div v-for="(educationType, index) in country.educationTypes" :key="'education-type-chip-' + index" class="text-center">
                    <v-chip class="ma-2" close color="primary" outlined @click:close="RemoveChip(index)" @click="SelectChip(index)">
                        {{educationType.name}}
                    </v-chip>
                </div>
            </v-row>
        </v-container>
    </v-form>
</template>

<script>
import validateMixin from '@/mixins/validate.js'

export default {
    name: "CountryStepOne",

    mixins: [validateMixin],

    data: () => ({
        country: {
            id: 0,
            name: null,
            educationTypes: []
        },
        educationYears: [],
        educationType: {
            id: 0,
            name: ""
        }
    }),

    mounted() {
        this.$eventBus.$on("show-step-content-1", this.InitData);
    },

    methods: {
        InitData(){
            if(this.$refs['form']){
                this.$refs['form'].resetValidation();
            }
        },
        AddChip(){
            if(this.$refs['form'].validate()){
                var type = {
                    id: this.educationType.id,
                    name: this.educationType.name,
                    educationYears: this.educationYears
                }
                this.country.educationTypes.push(type);
                this.educationType.name = null;
                this.educationType.id = 0;
                this.$refs['form'].resetValidation();
            }
        },
        SelectChip(index){
            var type = {
                id: this.country.educationTypes[index].id,
                name: this.country.educationTypes[index].name
            }
            this.educationType = type;
            this.RemoveChip(index);
        },
        RemoveChip(index){
            this.country.educationTypes.splice(index, 1);
        },
        validate(){
            return (!!this.country.name && this.country.educationTypes.length > 0) 
            || (this.$refs['form'].validate() && !!this.country.name && this.country.educationTypes.length > 0)
        }
    }
}
</script>

<style scoped>

</style>