<template>
    <modal :dialog="dialog" :title="$locales.t('teacherYearModal.deleteTitle')" @close="ModalClose" @done="ModalDone" width="500" class="text-center">
        <p class="title mt-5 text-center">{{$locales.t('teacherYearModal.deleteText')}}</p>
        <p class="error--text text-center">{{$locales.t('teacherYearModal.deleteNotes')}}</p>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import ValidationMixin from '@/mixins/validate.js'

export default {
    name: "DeleteTeacherYearModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog', 'course'],

    data() {
        return {
            date: false,
            teacherSubject: {
                educationYearId: 0,
                semesterId: 0,
                subjectId: 0
            }
        }
    },
    
    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            console.log(this.course)
            this.HttpRequest({
                url: 'api/teacher/course/',
                method: 'DELETE',
                data: this.course,
                auth: true,
                successNotify: true
            }).then(this.$emit("done"))
            .catch(this.$emit("close"));
        }
    }
}
</script>