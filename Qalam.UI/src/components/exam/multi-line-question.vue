<template>
    <div class="question-point question-type-multi-line">
        <div class="question-text">
            <h3>
                {{pointText}}
            </h3>
        </div>
        <div class="question-answers mt-4 col-12 col-md-6">
            <v-textarea v-model="answer"
                :label="label" auto-grow outlined />
        </div>
    </div>
</template>

<script>
export default {
    name: "MultiLineQuestion",

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
        answer: null
    }),

    computed: {
        pointText() {
            return `${this.pointNumber}- ${this.point.text}`;
        },
        label() {
            var result = "";
            switch(this.lang.toLowerCase()){
                case "AR":
                    result = 'الأجابة';
                    break;
                case "EN":
                    result = 'Answer';
                    break;
                case "FR":
                    result = 'Répondre';
                    break;
                case "IT":
                    result = 'Risposta';
                    break;
                case "DE":
                    result = 'Antworten';
                    break;
            }
            return result;
        }
    }
}
</script>

<style scoped>

</style>