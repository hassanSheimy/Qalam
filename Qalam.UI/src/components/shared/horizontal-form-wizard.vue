<template>
    <v-stepper v-model="currentStep" v-bind="$attrs" @change="change">
        <v-stepper-header>
            <template v-for="(step, index) in steps">
                <v-stepper-step complete-icon="far fa-check-circle" :complete="currentStep > (index + 1)" :step="index + 1" :key="'step-' + index">{{step}}</v-stepper-step>
                <v-divider :key="'divider-' + index" v-if="index + 1 < steps.length"></v-divider>
            </template>
        </v-stepper-header>

        <v-stepper-items>
            <v-stepper-content v-for="(step, index) in steps" :key="'step-content-' + index" :step="index + 1">
                <slot :name="'step-content-' + (index + 1)"></slot>
                <v-btn v-if="useDefaults && index + 1 != steps.length" color="primary" 
                    @click="NextStep(index)"
                    :loading="loading"
                    :disabled="loading">
                    {{$locales.t('nextBtn')}}
                </v-btn>
                <v-btn v-if="useDefaults && index + 1 == steps.length" color="primary"
                    @click="FormSubmit"
                    :loading="loading"
                    :disabled="loading">
                    {{$locales.t('submitBtn')}}
                </v-btn>
                <v-btn v-if="useDefaults && index != 0" text @click="PrevStep()" :disabled="isPrevDisabled">{{$locales.t('prevBtn')}}</v-btn>
            </v-stepper-content>
        </v-stepper-items>
    </v-stepper>
</template>

<script>
export default {
    name: 'HorizontalFormWizard',

    props: {
        steps: {
            type: Array,
            required: true,
            validator: (value) => {
                return value.length > 0
            }
        },
        useDefaults: {
            type: Boolean,
            default: true
        },
        validateFunction: {
            type: Function,
            default: (index) => {
                return true;
            }
        },
        submit: {
            type: Function,
            required: true
        }
    },

    data: () => ({
        currentStep: 1,
        loading: false,
        error: false,
        isPrevDisabled: false
    }),

    mounted() {
        this.$eventBus.$on("show-wizzard", this.InitWizard);
    },

    methods: {
        InitWizard(){
            this.currentStep = 1;
            this.$eventBus.$emit("show-step-content-" + this.currentStep);
        },
        NextStep(index) {
            this.loading = true;
            if(this.validateFunction(index)){
                this.currentStep = Math.min(this.steps.length, this.currentStep + 1);
                this.error = false
                this.$eventBus.$emit("show-step-content-" + this.currentStep);
            }
            else{
                this.error = true
            }
            this.loading = false;
        },
        PrevStep(){
            this.currentStep = Math.max(this.currentStep - 1, 1);
            this.$eventBus.$emit("show-step-content-" + this.currentStep);
        },
        FormSubmit(){
            this.loading = true;
            if(this.validateFunction(this.steps.length - 1)){
                this.submit();
                this.error = false
            }
            else{
                this.error = true
            }
            this.loading = false;
        },
        change(e){
            // form changed
        }
    }
}
</script>

<style scoped>
</style>