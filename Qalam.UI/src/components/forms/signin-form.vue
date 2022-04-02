<template>
    <v-container class="fill-height justify-center text-center" fluid>
        <v-row align="center" justify="center" >
        <v-col cols="12" sm="8" md="5">
            <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat >
                    <v-icon></v-icon>
                    <v-toolbar-title>{{$locales.t('login')}}</v-toolbar-title>
                    <v-spacer />
                </v-toolbar>
            <v-card-text>
                <v-form class="justify-center mt-10" ref="form" lazy-validation>
                    <v-text-field
                    v-model="userDetails.email"
                    outlined
                    :label="$locales.t('email')"
                    prepend-icon="fas fa-user" 
                    type="email" 
                    :rules="validationRules.email"/>

                    <v-text-field
                    v-model="userDetails.password" outlined
                    :label="$locales.t('password')" 
                    prepend-icon="fas fa-shield-alt" 
                    :append-icon="showPassword ? 'far fa-eye' : 'far fa-eye-slash'"
                    :type="showPassword ? 'text' : 'password'"
                    @click:append="showPassword = !showPassword"
                    :rules="validationRules.required" />

                    <v-checkbox v-model="rememberMe" :label="$locales.t('rememberMe')"
                    color="primary" :value="true"
                    hide-details class="mr-8" />
                </v-form>
            </v-card-text>
            <v-card-actions>
                <v-container>
                    <v-row no-gutters>
                        <v-col md="5" cols="12">
                            <div>
                                <v-btn class="subtitle-1" to="/forget-password" text style="text-align:right">
                                    {{$locales.t('forgetPassword')}}
                                </v-btn>
                                <v-spacer />
                                <v-btn class="subtitle-1" to="/signup" text style="text-align:right">
                                    {{$locales.t('noAccount')}}
                                </v-btn>
                            </div>
                        </v-col>
                        <v-spacer />
                        <v-col md="5" cols="12">
                            <v-btn 
                            color="primary" 
                            :loading="loading"
                            :disabled="loading"
                            @click="Login">
                                {{$locales.t('login')}}
                            </v-btn>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-actions>
            </v-card>
        </v-col>
        </v-row>
    </v-container>
</template>

<script>
import validateMixin from '@/mixins/validate.js'

export default {
    name: "SigninForm",

    mixins: [validateMixin],

    data: () => ({
        loading: false,
        showPassword: false,
        userDetails: {
            email: null,
            password: null
        },
        rememberMe: false
    }),

    methods: {
        Login() {
            if(this.$refs['form'].validate()){
                this.loading = true;
                this.HttpRequest({
                    url: "api/user/login",
                    method: "POST",
                    data: this.userDetails
                }).then(this.LoginSuccess)
                .catch(this.LoginFailed);
            }
        },
        LoginSuccess(response) {
            if(!response.data.roles || response.data.roles.length == 0){
                this.LoginFailed();
                return;
            }
            else if(this.rememberMe) {
                this.LongSession({
                    userName: response.data.userName, 
                    fullName: response.data.fullName, 
                    roles: response.data.roles, 
                    token: response.data.token
                });
            }
            else {
                this.ShortSession({
                    userName: response.data.userName,
                    fullName: response.data.fullName, 
                    roles: response.data.roles, 
                    token: response.data.token
                });
            }

            this.$qalamHub.$emit('reconnect', response.data.userName);

            this.$router.push({
                name: "home"
            });
            this.loading = false;
        },
        LoginFailed(error){
            this.loading = false;
        }
    }
}
</script>

<style scoped>
.fill-height{
    min-height: 100vh;
}

.link{
    color: #000;
    text-decoration: none;
}

.height{
    min-height: 350px;
}
</style>