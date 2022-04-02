<template>
    <v-form class="justify-center" ref="form" :lazy-validation="true" >
        <v-container fluid>
            <v-row no-gutters>
                <v-col cols="12" align-self="center">
                    <v-text-field
                    class="mt-2" outlined
                    v-model="educationYear.name" :rules="validationRules.required"
                    :label="$locales.t('educationYear')" prepend-icon="fas fa-graduation-cap"
                    required />
                </v-col>

                <v-col cols="12" sm="8">
                    <v-text-field class="mt-2" outlined
                    v-model="educationYear.numberOfSemesters"
                    :rules="ValidateNumberLessThan(13)" counter="2"
                    :label="$locales.t('numberOfSemesters')"
                    prepend-icon="fas fa-graduation-cap" required />
                </v-col>

                <v-col cols="6" sm="1" align-self="center">
                    <v-btn outlined @click="AddChip" color="primary" class="ma-4">
                        <v-icon left color="primary" size="16">fas fa-plus</v-icon>
                        {{$locales.t('addBtn')}}
                    </v-btn>
                </v-col>
            </v-row>
            <v-row no-gutters>
                <div v-for="(educationYear, index) in educationYears" :key="'education-year-row-' + index" class="text-center">
                    <v-chip class="ma-2" close color="primary" outlined @click:close="RemoveChip(index)" @click="SelectChip(index)">
                        {{educationYear.name}}
                    </v-chip>
                </div>
            </v-row>
        </v-container>
    </v-form>
</template>

<script>
import validateMixin from '@/mixins/validate.js'

export default {
    name: "CountryStepTwo",

    mixins: [validateMixin],

    data: () => ({
        educationYear: {
            id: 0,
            name: null,
            numberOfSemesters: null
        },
        educationYears: []
    }),

    mounted() {
        this.$eventBus.$on("show-step-content-2", this.InitData);
    },

    methods: {
        InitData(){
            if(this.$refs['form']){
                this.$refs['form'].resetValidation();
            }
        },
        AddChip(){
            if(this.$refs['form'].validate()){
                var year = {
                    id: this.educationYear.id,
                    name: this.educationYear.name,
                    numberOfSemesters: parseInt(this.educationYear.numberOfSemesters)
                }
                this.educationYears.push(year);
                this.educationYear.name = null;
                this.educationYear.numberOfSemesters = null;
                this.educationYear.id = 0;
                this.$refs['form'].resetValidation();
            }
        },
        SelectChip(index){
            var year = {
                id: this.educationYears[index].id,
                name: this.educationYears[index].name,
                numberOfSemesters: this.educationYears[index].numberOfSemesters
            }
            this.educationYear = year;
            this.RemoveChip(index);
        },
        RemoveChip(index){
            this.educationYears.splice(index, 1);
        },
        validate(){
            return (this.educationYears.length > 0) 
            || (this.$refs['form'].validate() && this.educationYears.length > 0)
        }
    }
}
</script>

<style scoped>

</style>