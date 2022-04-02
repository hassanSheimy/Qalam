<template>
   <v-form class="justify-center" ref="form" :lazy-validation="true" >
        <v-container fluid>
            <v-row>
                <v-col cols="12">
                    <v-text-field outlined v-model="subject.name" :rules="validationRules.required"
                    :label="$locales.t('subject')" prepend-icon="fas fa-book"
                    required />
                </v-col>

                <v-col cols="12" md="6">
                    <v-select :no-data-text="$locales.t('noData')" outlined
                    item-text="name" v-model="subject.educationTypes"
                    prepend-icon="fas fa-school" :items="educationTypes"
                    :rules="validationRules.atLeastOne" :label="$locales.t('educationType')"
                    multiple chips required return-object />
                </v-col>

                <v-col cols="12" md="6">
                    <v-select :no-data-text="$locales.t('noData')" outlined
                    item-text="name" v-model="subject.educationYears"
                    prepend-icon="fas fa-graduation-cap" :items="educationYears"
                    :rules="validationRules.atLeastOne" :label="$locales.t('educationYear')" 
                    required multiple chips return-object />
                </v-col>
                
                <v-col cols="12" sm="9">
                    <v-select :no-data-text="$locales.t('noData')" outlined
                    v-model="subject.languageId" prepend-icon="fas fa-language"
                    item-text="name" item-value="id"
                    :items="languages" :rules="validationRules.required" 
                    :label="$locales.t('language')" />
                </v-col>

                <v-col cols="6" sm="1" align-self="center">
                    <v-btn outlined @click="AddChip" color="primary" class="ma-4">
                        <v-icon left color="primary" size="16">fas fa-plus</v-icon>
                        {{$locales.t('addBtn')}}
                    </v-btn>
                </v-col>
            </v-row>
            <v-row no-gutters>
                <div v-for="(subject, index) in subjects" :key="'education-year-row-' + index" class="text-center">
                    <v-chip class="ma-2" close color="primary" outlined @click:close="RemoveChip(index)"  @click="SelectChip(index)">
                        {{subject.name}}
                    </v-chip>
                </div>
            </v-row>
        </v-container>
    </v-form>
</template>

<script>
import ValidateMixin from '@/mixins/validate.js'

export default {
    name: "CountryStepThree",

    mixins: [ValidateMixin],

    data: () => ({
        subject: {
            id: 0,
            name: null,
            languageId: 0,
            educationYears: [],
            educationTypes: []
        },
        subjects: [],
        educationTypes: [],
        educationYears: [],
    }),

    computed: {
        languages() {
            return this.$store.getters.Languages || [];
        }
    },

    mounted() {
        this.$eventBus.$on("show-step-content-3", this.InitData);
    },

    methods: {
        InitData() {
            if(this.$refs['form']){
                this.$refs['form'].resetValidation();
            }
        },
        AddChip(){
            if(this.$refs['form'].validate()){
                var subject = {
                    id: this.subject.id,
                    name: this.subject.name,
                    languageId: this.subject.languageId,
                    educationTypes: [...this.subject.educationTypes],
                    educationYears: [...this.subject.educationYears]
                }
                this.subjects.push(subject);
                this.subject.name = null;
                this.subject.languageId = null;
                this.subject.id = 0;
                this.subject.educationTypes = [];
                this.subject.educationYears = [];
                this.$refs['form'].resetValidation();
            }
        },
        SelectChip(index){
            var subject = {
                id: this.subjects[index].id,
                name: this.subjects[index].name,
                languageId: this.subjects[index].languageId,
                educationTypes: [...this.subjects[index].educationTypes],
                educationYears: [...this.subjects[index].educationYears]
            }
            this.subject = subject;
            this.RemoveChip(index);
        },
        RemoveChip(index){
            this.subjects.splice(index, 1);
        },
        validate(){
            return (this.subjects.length > 0) 
            || (this.$refs['form'].validate() && this.subjects.length > 0)
        }
    }
}
</script>

<style scoped>

</style>