<template>
    <div class="question" :dir="RTL ? 'rtl' : 'ltr'">
        <div class="question-header">
            <h2 class="">
               {{questionTitle}}
            </h2>
            <p class="question-header-hint">{{questionHint}}</p>
        </div>
        <br />
        <div class="question-body ">
            <slot />
        </div>
    </div>
</template>

<script>
export default {
    name: "QuestionContainer",

    props: {
        RTL: {
            type: Boolean,
            default: true
        },
        lang: {
            type: String,
            default: "AR"
        },
        questionNumber: {
            type: Number,
            required: true
        },
        title: {
            type: String,
            required: true
        },
        questionHint: {
            type: String
        }
    },
    
    computed: {
        questionTitle(){
            var result = "";
            switch(this.lang.toLowerCase()){
                case "AR":
                    result = `السؤال رقم (${this.questionNumber})`;
                    break;
                case "EN":
                    result = `Question No. (${this.questionNumber})`;
                    break;
                case "FR":
                    result = `Question No. (${this.questionNumber})`;
                    break;
                case "IT":
                    result = `Domanda n. (${this.questionNumber})`;
                    break;
                case "DE":
                    result = `Frage Nr. (${this.questionNumber})`;
                    break;
            }
            result += `: ${this.title}`;
            return result;
        }
    }
}
</script>

<style scoped>
.question {
    margin-top: 20px !important;
}
</style>