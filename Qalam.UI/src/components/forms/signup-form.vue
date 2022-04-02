<template>
    <v-container class="fill-height justify-center" fluid>
        <v-row align="center" justify="center" >
            <v-col cols="12" lg="6" sm="8">
                <v-card class="elevation-12">
                    
                    <v-toolbar color="primary" dark flat >
                        <v-toolbar-title>{{$locales.t('signupForm.title')}}</v-toolbar-title>
                        <v-spacer />
                    </v-toolbar>
                    
                    <v-card-text>
                        <p v-if="errorMessages.length > 0" class="error--text px-5">
                            <ul>
                                <li v-for="error in errorMessages" :key="'error-' + error">{{ error }}</li>
                            </ul>
                        </p>
                    
                        <horizontal-form-wizard :steps="$locales.t('signupForm.steps')" :submit="Submit" 
                        :validateFunction="FormValidate">
                            
                            <step-one ref="step-1" slot="step-content-1" class="step-content" />
                            <step-two ref="step-2" slot="step-content-2" class="step-content" />
                            <step-three ref="step-3" slot="step-content-3" class="step-content" />
                            
                        </horizontal-form-wizard>
                    </v-card-text>

                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import HorizontalFormWizard from '@/components/shared/horizontal-form-wizard.vue'
import LookupsMixin from '@/mixins/lookups.js'
import StepOne from '@/components/form-steps/signup-step-one.vue'
import StepTwo from '@/components/form-steps/signup-step-two.vue'
import StepThree from '@/components/form-steps/signup-step-three.vue'

export default {
    name: "SignupForm",

    mixins: [LookupsMixin],

    components: {
        HorizontalFormWizard,
        StepOne,
        StepTwo,
        StepThree
    },

    data: () => ({
        currentStep: 1,
        date: false,
        userDetails: {
            firstName: '',
            lastName: '',
            phoneNumber: '',
            birthDate: '',
            userName: '',
            email: '',
            password: '',
            repeatPassword: '',
            type: 0,
            countryId: 0,
            educationTypeId: 0,
            student: {
                parentPhoneNumber: null,
                educationYearId: 0
            },
            teacher: {
                subjectId: 0,
            },
            roles: []
        },
        loading: true,
        errorMessages: []
    }),

    mounted() {
        this.$refs["step-1"].userDetails = this.userDetails;
        this.$refs["step-2"].userDetails = this.userDetails;
        this.$refs["step-3"].userDetails = this.userDetails;
        this.$refs["step-3"].lookups = this.lookups;
    },

    methods: {
        FormValidate(index){
            var result = true;
            switch(index){
                case 0:
                    result = this.$refs["step-1"].validate();
                    break;
                case 1:
                    result = this.$refs["step-2"].validate();
                    break;
                case 2:
                    result = this.$refs["step-3"].validate();
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        },
        Submit(){
            this.userDetails.countryId = this.lookups.country;
            this.userDetails.educationTypeId = this.lookups.educationType;
            
            if(this.userDetails.type == 1){
                this.userDetails.roles.push({
                    name: 'Teacher', 
                    normalizedName: 'TEACHER'
                })
                this.userDetails.student = null;
                this.userDetails.teacher.subjectId = this.lookups.countrySubject;
            }
            else{
                this.userDetails.roles.push({ 
                    name: 'Student', 
                    normalizedName: 'STUDENT' 
                })
                this.userDetails.student.educationYearId = this.lookups.educationYear;
                this.userDetails.teacher = null;
            }
            this.HttpRequest({
                url: "api/user/registration",
                method: "POST",
                data: this.userDetails
            }).then(this.PostSuccess)
            .catch(this.PostFaild);
        },
        PostSuccess(response){
            if(!response.data.roles || response.data.roles.length == 0){
                this.PostFaild();
                return;
            }

            this.ShortSession({
                userName: response.data.userName, 
                fullName: response.data.fullName, 
                roles: response.data.roles, 
                token: response.data.token
            });
            this.$router.push({
                name: "home"
            });
        },
        PostFaild(error){
            // error happend
            if(error && error.data && error.data.statusCode){
                switch(error.data.statusCode){
                    case 14:
                        this.errorMessages.push(this.$locales.t('userNameExists'))
                        break;
                    case 15:
                        this.errorMessages.push(this.$locales.t('emailExists'))
                        break;
                    case 16:
                        this.errorMessages.push(this.$locales.t('userNameExists'))
                        this.errorMessages.push(this.$locales.t('emailExists'))
                        break;
                }
            
            }
        }
    }
}
</script>

<style scoped>
.fill-height{
    min-height: 100vh;
}

.rules-checkbox{
    margin: 20px;
}
</style>