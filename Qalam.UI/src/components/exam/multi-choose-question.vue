<template>
    <div class="question-point question-type-choose">
        <div class="question-text">
            <h3>
                {{pointText}}
            </h3>
        </div>
        <div class="question-answers mt-4">
            <v-row no-gutters>
                <v-col v-for="(answer, index) in point.answers" :key="`answer-${index}`" cols="12" md="6">
                    <v-radio-group v-model="selectedAnswer" class="radio">
                        <v-radio :label="answer.text" :value="answer.id"></v-radio>
                    </v-radio-group>
                </v-col>
            </v-row>
        </div>
    </div>
</template>

<script>
export default {
    name: "MultiChooseQuestion",

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
        selectedAnswer: null
    }),

    computed: {
        pointText() {
            return `${this.pointNumber}- ${this.point.text}`;
        }
    }
}
</script>

<style scoed>
.radio label{
    margin: 10px
}
</style>