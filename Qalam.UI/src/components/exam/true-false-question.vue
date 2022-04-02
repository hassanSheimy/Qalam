<template>
    <div class="question-point question-type-true-false">
        <div class="question-text">
            <h3>
                {{pointText}}
            </h3>
        </div>
        <div class="question-answers mt-4">
            <v-row no-gutters>
                <v-col v-for="(answer, index) in localizeAnswers" :key="`answer-${index}`" cols="12" md="6">
                    <v-radio-group v-model="selectedAnswer" class="radio">
                        <v-radio :label="answer.text" :value="answer.value"></v-radio>
                    </v-radio-group>
                </v-col>
                <v-col cols="12" md="6" v-if="point.hasCorrection" class="mt-4">
                    <v-text-field v-model="correctionAnswer"
                        :label="label"
                        outlined
                    ></v-text-field>
                </v-col>
            </v-row>
        </div>
    </div>
</template>

<script>
export default {
    name: "TrueFalseQuestion",

    props: {
        RTL: {
            type: Boolean,
            default: true
        },
        lang: {
            type: String,
            default: "AR"
        },
        pointNumber: {
            type: Number,
            required: true
        },
        point: {
            type: Object,
            required: true
        }
    },

    data: () => ({
        selectedAnswer: null,
        correctionAnswer: null
    }),

    computed: {
        pointText() {
            return `${this.pointNumber}- ${this.point.text}`;
        },
        label() {
            var result = "";
            switch(this.lang.toLowerCase()){
                case "AR":
                    result = 'التصحيح';
                    break;
                case "EN":
                    result = 'Correction';
                    break;
                case "FR":
                    result = 'Correction';
                    break;
                case "IT":
                    result = 'Correzione';
                    break;
                case "DE":
                    result = 'Korrektur';
                    break;
            }
            return result;
        },
        localizeAnswers() {
            switch(this.lang.toLowerCase()){
                case "AR":
                    return [
                        {text: "العباره صحيحة", value: true},
                        {text: "العباره خاظئة", value: false}
                    ];
                case "EN":
                    return [
                        {text: "True", value: true},
                        {text: "False", value: false}
                    ];
                case "FR":
                    return [
                        {text: "Vrai", value: true},
                        {text: "Faux", value: false}
                    ];
                case "IT":
                    return [
                        {text: "Vero", value: true},
                        {text: "Falso", value: false}
                    ];
                case "DE":
                    return [
                        {text: "Wahr", value: true},
                        {text: "Falsch", value: false}
                    ];
            }
        }
    }
}
</script>

<style scoped>
.radio label{
    margin: 10px
}
</style>