<template>
    <v-stepper v-model="currentStep" vertical v-bind="$attrs" >
        <template v-for="(step, index) in steps">
            <v-stepper-step editable edit-icon="fas fa-pen" :key="'step-' + index" :complete="currentStep > (index + 1)" :step="index + 1">
                {{step.title}}
            </v-stepper-step>

            <v-stepper-content :key="'content-' + index" :step="(index + 1)">
                <slot :name="'step-content-' + (index + 1)"></slot>

                <v-btn v-if="useDefaults && index + 1 != steps.length" color="primary" @click="NextStep(index)">
                    التالي
                </v-btn>
                <v-btn v-if="useDefaults && index + 1 == steps.length" color="primary" @click="Submit">
                    تأكيد
                </v-btn>
                <v-btn v-if="useDefaults && index != 0" text @click="currentStep = index">السابق</v-btn>
            </v-stepper-content>
        </template>
    </v-stepper>
</template>

<script>
export default {
    name: "VerticalFormWizard",

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
        }
    },

    data: () => ({
        currentStep: 1
    }),

    methods: {
        Valid(index){
            return !this.steps[index].isForm || this.steps[index].form.validate()
        },
        NextStep(index) {
            if(this.Valid(index)){
                this.currentStep = (index + 1 == this.steps.length ? index + 1 :index + 2)
            }
        },
        Submit(){
            if(this.Valid(this.steps.length - 1)){
                
            }
        }
    }
}
</script>

<style>
.v-stepper--vertical .v-stepper__content{
    padding: 24px 5px 16px 24px !important;
}
</style>