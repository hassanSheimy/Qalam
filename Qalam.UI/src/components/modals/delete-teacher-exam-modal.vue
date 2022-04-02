<template>
    <modal :dialog="dialog" title="حذف الامتحان" @close="ModalClose" @done="ModalDone" width="500">
        <p class="subtitle-1">هل انت متأكد انك تريد حذف الامتحان؟</p>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import ValidationMixin from '@/mixins/validate.js'

export default {
    name: "DeleteTeacherExamModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog', 'exam'],

    data() {
        return {
            teacherExam: {
                name: '',
                subjectId: 0,
                lessonId: 0,
                date: '',
                timeInMinutes: null
            }
        }
    },

    mounted() {
        this.teacherExam.name = this.exam.name
        this.teacherExam.lessonId = this.exam.lessonId
        this.teacherExam.date = this.exam.date
        this.teacherExam.timeInMinutes = this.exam.timeInMinutes
        this.teacherExam.id = this.exam.id
    },
    
    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            this.$emit("done");
        }
    }
}
</script>