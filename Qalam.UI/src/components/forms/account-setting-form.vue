<template>
    <custom-card class="v-card-profile text-right" color="primary" title="اعدادات الحساب" text="يمكنك تعديل البيانات التالية" customClass="white--text px-5 py-3">
        <v-form class="justify-center" ref="form" :lazy-validation="true" >
            <v-container fluid>
                <v-row>
                    <v-col md="4" cols="12">
                        <v-text-field outlined disabled v-model="userDetails.userName"
                        :rules="validationRules.name" label="اسم المستخدم" 
                        prepend-icon="fas fa-user" required />
                    </v-col>
                    <v-col md="4" cols="12">
                        <v-text-field outlined v-model="userDetails.firstName"
                        :counter="32" :rules="validationRules.name"
                        label="الأسم الأول" prepend-icon="fas fa-user" required/>
                    </v-col>
                    <v-col md="4" cols="12">
                        <v-text-field outlined v-model="userDetails.lastName"
                        :counter="32" :rules="validationRules.name"
                        label="الأسم الأخير" prepend-icon="fas fa-user"
                        required />
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-text-field outlined v-model="userDetails.phoneNumber"
                        :rules="validationRules.phoneNumber" label="رقم الهاتف"
                        prepend-icon="fas fa-phone" required />
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-menu
                            v-model="date"
                            :close-on-content-click="false"
                            max-width="290">
                            <template v-slot:activator="{ on }">
                                <v-text-field outlined v-model="userDetails.birthDate"
                                label="تاريخ الميلاد" prepend-icon="fas fa-birthday-cake"
                                :rules="validationRules.date" v-on="on"
                                readonly @click:clear="date = null" />
                            </template>
                            <v-date-picker
                            v-model="userDetails.birthDate"
                            @change="date = false"/>
                        </v-menu>
                    </v-col>
                    <v-col :md="auth.cols" cols="12">
                        <v-select disabled v-model="lookups.country"
                        item-text="name" item-value="id"
                        :no-data-text="$locales.t('noData')" outlined
                        :items="countries" prepend-icon="fas fa-globe-africa" label="الدوله" 
                        required />
                    </v-col>
                    <v-col v-if="auth.isStudent || auth.isTeacher" :md="auth.cols" cols="12">
                        <v-select v-model="lookups.educationType" outlined
                        item-text="name" item-value="id" disabled
                        :no-data-text="$locales.t('noData')" :items="educationTypes" 
                        :rules="validationRules.required" prepend-icon="fas fa-school"
                        label="نظام التعليم" required />
                    </v-col>
                    <v-col v-if="auth.isStudent" :md="auth.cols" cols="12">
                        <v-select v-model="lookups.educationYear" outlined
                        item-text="name" item-value="id"
                        :no-data-text="$locales.t('noData')" :items="educationYears" 
                        :rules="validationRules.required" prepend-icon="fas fa-school"
                        label="السنة الدراسية" required />
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-text-field outlined v-model="userDetails.facebook"
                        :rules="validationRules.url" label="حساب فيس بوك"
                        prepend-icon="fab fa-facebook-square" required />
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-text-field outlined v-model="userDetails.linkedIn"
                        :rules="validationRules.url" label="حساب لينكدان" 
                        prepend-icon="fab fa-linkedin" required />
                    </v-col>
                    <v-col cols="12">
                        <v-textarea v-model="userDetails.about" outlined
                        :counter="500" clearable no-resize
                        label="نبذه عني" rows="5"
                        prepend-icon="fas fa-comment" />
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
        <v-card-actions>
            <v-spacer />
            <v-btn 
                color="primary" 
                :loading="loading"
                :disabled="loading"
                @click="SaveUserData">حفظ التغييرات</v-btn>
        </v-card-actions>
    </custom-card>
</template>

<script>
import validateMixin from '@/mixins/validate.js'
import CustomCard from '@/components/cards/custom-card.vue'
import LookupsMixin from '@/mixins/lookups.js'

export default {
    name: "AccountSettingForm",

    components: {
        CustomCard
    },

    mixins: [validateMixin, LookupsMixin],

    props: {
        role: {
            type: String,
            required: true
        }
    },

    data: () => ({
        date: false,
        loading: false,
        userDetails: {}
    }),

    computed: {
        auth() {
            var result = {
                isStudent: false,
                isTeacher: false,
                cols: 12
            };
            var role = this.$route.path.toLowerCase().split("/")[2];
            if(role == "student"){
                result.isStudent = true;
                result.cols = 4;
            }
            else if(role == "teacher"){
                result.isTeacher = true;
                result.cols = 6;
            }
            return result;
        }
    },

    created() {
        this.$eventBus.$on('user-details-updated', this.InitForm);
    },

    mounted() {
        this.InitForm()
    },

    methods: {
        InitForm(){
            if(this.$refs.form != null){
                this.$refs.form.resetValidation();
            }
            this.userDetails = this.$store.getters.UserDetails
            this.lookups.country = this.userDetails.countryId;
            this.lookups.educationType = this.userDetails.educationTypeId;
            this.lookups.educationYear = this.userDetails.educationYearId;
        },
        SaveUserData() {
            this.loading = true;
            this.HttpRequest({
                url: 'api/user',
                method: 'PUT',
                data: {
                    firstName: this.userDetails.firstName,
                    lastName: this.userDetails.lastName,
                    phoneNumber: this.userDetails.phoneNumber,
                    birthDate: this.userDetails.birthDate,
                    educationYearId: this.auth.isStudent ? this.lookups.educationYear : null,
                    about: this.userDetails.about,
                    facebook: this.userDetails.facebook,
                    linkedIn: this.userDetails.linkedIn
                },
                auth: true,
                successNotify: true,
                loading: false
            }).then(this.EditDone)
            .catch(this.EditFaild);
        },
        EditDone(response){
            this.loading = false;
            this.UpdateUser({
                'full-name': `${this.userDetails.firstName} ${this.userDetails.lastName}`
            });
            this.userDetails.educationYearId = this.auth.isStudent ? this.lookups.educationYear : this.userDetails.educationYearId
            this.$store.commit("UpdateUserData", this.userDetails);
            this.$eventBus.$emit('user-details-updated');
        },
        EditFaild(error){
            this.loading = false;
        }
    }
}
</script>

<style scoped>
.v-card-profile {
    margin-top: 0 !important;
}
</style>