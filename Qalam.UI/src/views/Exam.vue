<template>
    <div class="Exam">
        <default-header />
        <exam-layout :RTL="exam.RTL" :questions="exam.questions" :lang="exam.lang" :title="exam.title" :time="exam.time" :timeUnit="exam.timeUnit" />
        <secondary-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue';
import ExamLayout from '@/components/exam/exam-layout.vue';
import SecondaryFooter from '@/components/layout/secondary-footer.vue';

export default {
    name: "Exam",

    components: {
        DefaultHeader,
        ExamLayout,
        SecondaryFooter
    },

    data: () => ({
        exam: {
            title: "امتحان موقع قلم لمادة العربي",
            time: 1,
            timeUnit: "دقيقه",
            RTL: true,
            lang: "AR",
            questions: [{
                title: "اختر الإجابة الصحيحة",
                type: "1",
                points: [{
                    text: "يتكون قلب الإنسان من ..... حجرات",
                    answers:[{
                        text: "سته",
                        id: 1
                    },{
                        text: "ثلاثة",
                        id: 2
                    },{
                        text: "اربعة",
                        id: 3
                    },{
                        text: "خمسة",
                        id: 4
                    }]
                }]
            },{
                title: "اجب بصح او خطأ",
                type: "2",
                points: [{
                    text: "يتكون قلب الإنسان من 4 حجرات",
                    hasCorrection: false
                }]
            },{
                title: "اكمل ما يلي",
                type: "3",
                points: [{
                    text: "يتكون قلب الإنسان من ...."
                }]
            },{
                title: "علل لما يأتي",
                type: "4",
                points: [{
                    text: "قلب الإنسان يتكون من اربعة حجرات"
                }]
            }]
        }
    }),
}
</script>

<style scoped>
.exam-body {
    background-color: white;
    margin-top: 50px !important;
    padding: 20px;
}
</style>