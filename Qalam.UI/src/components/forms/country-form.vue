<template>
    <horizontal-form-wizard :steps="$locales.t('countryForm.steps')" class="mt-4" 
    :validateFunction="WizardValidateFunction" :submit="Submit">

        <step-one ref="step-1" slot="step-content-1" class="step-content-1 full-height" />
        <step-two ref="step-2" slot="step-content-2" class="step-content-2 full-height" />
        <step-three ref="step-3" slot="step-content-3" class="step-content-3 full-height" />
        
    </horizontal-form-wizard>
</template>

<script>
import HorizontalFormWizard from '@/components/shared/horizontal-form-wizard.vue'
import StepOne from '@/components/form-steps/country-step-one.vue'
import StepTwo from '@/components/form-steps/country-step-two.vue'
import StepThree from '@/components/form-steps/country-step-three.vue'

export default {
    name: "CountryForm",
    
    components: {
        HorizontalFormWizard,
        StepOne,
        StepTwo,
        StepThree
    },

    props: ['old-country'],

    data: () => ({
        country: {
            id: 0,
            name: null,
            educationTypes: [],
            educationYears: [],
            subjects: [],
            courses: []
        },
        isEdit: false
    }),

    created() {
        this.$eventBus.$on("InitForm", this.InitForm);
    },

    mounted() {
        this.InitForm(this.oldCountry);
    },

    methods: {
        InitForm(country){
            if(country){
                this.isEdit = true;
                this.country = Object.assign({}, country);
            }
            else{
                this.isEdit = false;
                this.country = {
                    id: 0,
                    name: null,
                    educationTypes: [],
                    educationYears: [],
                    subjects: [],
                    courses: []
                }
            }
            this.$eventBus.$emit("show-wizzard");
            
            this.$refs["step-1"].country = this.country;
            this.$refs["step-1"].educationYears = this.country.educationYears;
            this.$refs["step-2"].educationYears = this.country.educationYears;
            this.$refs["step-3"].educationTypes = this.country.educationTypes;
            this.$refs["step-3"].educationYears = this.country.educationYears;
            this.$refs["step-3"].subjects = this.country.subjects;
        },
        WizardValidateFunction(index){
            switch(index){
                case 0:
                    return this.$refs["step-1"].validate();
                    break;
                case 1:
                    return this.$refs["step-2"].validate()
                    break;
                case 2:
                    return this.$refs["step-3"].validate()
                    break
            }
        },
        Submit(){
            this.country.courses = [];
            
            this.country.subjects.forEach(subject => {
                subject.educationYears.forEach(year => {
                    subject.educationTypes.forEach(type => {
                        this.country.courses.push({
                            subjectName: subject.name,
                            typeName: type.name,
                            yearName: year.name
                        });
                    });
                });
            });

            this.HttpRequest({
                url: 'api/countries',
                method: this.isEdit ? 'PUT' : 'POST',
                data: this.country,
                auth: true,
                successNotify: true
            }).then(this.PostSuccess)
            .catch(this.PostFaild);
        },
        PostSuccess(response){
            this.$emit("done", this.country);
        },
        PostFaild(error){
            // add faild
            this.$emit("close");
        }
    },
}
</script>

<style scoped>
.full-height{
    height: 45vh;
    min-height: 45vh;
    max-height: 45vh;
    overflow-y: auto;
}
</style>