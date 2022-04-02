<template>
    <v-container class="exam-layout">
        <v-row class="exam-header" :dir="RTL ? 'rtl' : 'ltr'"><!-- Exam Header -->
            <v-col cols="6" class="exam-title text-sm-right text-center">
                <h1 class="title">{{title}}</h1>
            </v-col>
            <v-col cols="6" class="exam-timer text-sm-left text-center">
                <h1 class="title">{{priod}}</h1>
            </v-col>
        </v-row><!-- End Exam Header -->
        <v-row no-gutters>
            <v-col v-for="(question, index) in questions" :key="`question-container-${index}`" cols="12">
                <question-container :RTL="RTL" :questionNumber="(index + 1)" questionHint="" :title="question.title" :lang="lang"> 
                    
                    <div v-if="question.type == questionsType.multiplyChoose">
                        <multi-choose-question v-for="(point, index2) in question.points" 
                            :key="`multi-choose-question-${index}-point-${index2}`"
                            :point="point" :RTL="RTL" :pointNumber="(index2 + 1)" :lang="lang"/>
                    </div>

                    <div v-if="question.type == questionsType.trueOrFalse">
                        <true-false-question v-for="(point, index2) in question.points" 
                            :key="`true-false-question-${index}-point-${index2}`"
                            :point="point" :RTL="RTL" :pointNumber="(index2 + 1)" :lang="lang"/>
                    </div>

                    <div v-if="question.type == questionsType.text">
                        <text-question v-for="(point, index2) in question.points" 
                            :key="`multi-line-text-question-${index}-point-${index2}`"
                            :point="point" :RTL="RTL" :pointNumber="(index2 + 1)" :lang="lang" />
                    </div>

                    <div v-if="question.type == questionsType.multiplyLineText">
                        <multi-line-question v-for="(point, index2) in question.points" 
                            :key="`text-question-${index}-point-${index2}`"
                            :point="point" :RTL="RTL" :pointNumber="(index2 + 1)" :lang="lang" />
                    </div>

                </question-container>
            </v-col>
        </v-row>
        <modal :dialog="examEnded" @done="ExamDone" @close="ExamDone" title="انتهى الوقت" :width="500">
            <p class="mt-5 text-center title black--text">
                اضغط تأكيد او اغلق النافذه لتصحيح الإمتحان<br />
                قد يستغرق عرض النتيجه من 5 الى 10 دقائق
            </p>
        </modal>
    </v-container>
</template>

<script>
import QuestionContainer from '@/components/exam/question-container.vue'
import MultiChooseQuestion from '@/components/exam/multi-choose-question.vue'
import TrueFalseQuestion from '@/components/exam/true-false-question.vue'
import MultiLineQuestion from '@/components/exam/multi-line-question.vue'
import TextQuestion from '@/components/exam/text-question.vue'
import Modal from '@/components/shared/modal.vue'

export default {
    name: "ExamLayout",

    components: {
        QuestionContainer,
        MultiChooseQuestion,
        TrueFalseQuestion,
        MultiLineQuestion,
        TextQuestion,
        Modal
    },

    props: {
        title: {
            type: String,
            required: true
        },
        time: {
            type: Number,
            required: true
        },
        timeUnit: {
            type: String,
            required: true
        },
        RTL: {
            type: Boolean,
            default: true
        },
        lang: {
            type: String,
            default: "AR"
        },
        questions: {
            type: Array,
            required: true
        }
    },

    computed: {
        priod() {
            var result = "";
            switch(this.lang.toLowerCase()){
                case "AR":
                    result = `مدة الإمتحان`;
                    break;
                case "EN":
                    result = `Duration of the exam`;
                    break;
                case "FR":
                    result = `Durée de l'examen`;
                    break;
                case "IT":
                    result = `Durata dell'esame`;
                    break;
                case "DE":
                    result = `Dauer der Prüfung`;
                    break;
            }
            result += `: ${this.time} ${this.timeUnit}`;
            return result; 
        }
    },

    data: () => ({
        questionsType: {
            multiplyChoose: 1,
            trueOrFalse: 2,
            text: 3,
            multiplyLineText: 4
        },
        stopTime: null,
        countDown: '',
        snackbar: {
            appear: false,
            color: "primary",
            countOfAppearance: 0,
            text: ''
        },
        examEnded: false
    }),

    mounted() {
        this.stopTime = new Date("Feb 14, 2020 19:59:25").getTime();
        this.CountDown();
    },

    methods: {
        CountDown() {
            setInterval(function() {
                this.snackbar.countOfAppearance += 1;
                if(this.snackbar.countOfAppearance == 2){
                    this.snackbar.text = `مضى نصف الوقت و تبقى ${Math.round(this.time / 2)} ${this.timeUnit}`;
                    this.$root.$notify(this.snackbar)
                }

                else if(this.snackbar.countOfAppearance == 3){
                    this.snackbar.text = `تبقى ربع الوقت ${Math.round(this.time / 4)} ${this.timeUnit}`;
                    this.snackbar.color = 'warning';
                    this.$root.$notify(this.snackbar)
                }

                else if(this.snackbar.countOfAppearance == 4){
                    window.clearInterval();
                    // show popup to tell the user thats the exam has been ended
                    this.snackbar.text = `انتهى الوقت`;
                    this.snackbar.color = 'error';
                    this.$root.$notify(this.snackbar)
                    this.examEnded = true;
                }

            }.bind(this), this.time * 60 * 250)
        },
        ExamDone() {
            this.$router.push({
                name: 'student-analytics'
            });
        }
    }
}
</script>

<style scoped>
.exam-layout {
    background-color: white;
    margin-top: 50px !important;
    margin-bottom: 50px !important;
    padding: 20px;
    min-height: 85vh;
}
.exam-header{
    border-bottom: 1px solid #000
}
</style>