<template>
    <modal :dialog="dialog" title="إضافة امتحان جديد" @close="ModalClose" @done="ModalDone">
        <v-form ref="form" :lazy-validation="true">
            <v-container grid-list-md>
                <v-row no-gutters >
                    <v-col cols="12">
                        <v-text-field
                            outlined
                            v-model="exam.name"
                            prepend-icon="fas fa-file-alt"
                            :rules="validationRules.required" 
                            label="اسم الإمتحان" 
                            required>
                        </v-text-field>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="exam.subjectId"
                            prepend-icon="fas fa-book-open"
                            :items="lessons"
                            :rules="validationRules.required" 
                            label="المادة" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="exam.lessonId"
                            prepend-icon="fas fa-book-open"
                            :items="lessons"
                            :rules="validationRules.required" 
                            label="الدرس" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="exam.date"
                            prepend-icon="fas fa-calendar-day"
                            :items="date"
                            :rules="validationRules.required" 
                            label="الموعد" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-text-field
                            outlined
                            v-model="exam.timeInMinutes"
                            prepend-icon="fas fa-clock"
                            :rules="validationRules.number" 
                            label="مده الامتحان بالدقائق" 
                            required>
                        </v-text-field>
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import ValidationMixin from '@/mixins/validate.js'

export default {
    name: "AddTeacherExamModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog'],

    data() {
        return {
            date: [
                { key: "قبل الدرس", value: 1 },
                { key: "بعد الدرس", value: 2 },
            ],
            subjects: [
                {key: 'اللغة العربية', value: 1}
            ],
            lessons: [
                {key: 'اللغة العربية', value: 1}
            ],
            exam: {
                name: '',
                subjectId: 0,
                lessonId: 0,
                date: '',
                timeInMinutes: null
            }
        }
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