<template>
    <modal :dialog="dialog" title="طلب تقديم درس" @close="ModalClose" @done="ModalDone">
        <v-form ref="form" :lazy-validation="true">
            <v-container grid-list-md>
                <v-row no-gutters >
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.isLive"
                            prepend-icon="fas fa-video"
                            :items="videoTypes"
                            :rules="validationRules.required" 
                            label="نوع الفيديو" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-menu v-model="date" :close-on-content-click="false" max-width="290">
                        <template v-slot:activator="{ on }">
                            <v-text-field
                                v-model="lessonVideo.showDate"
                                outlined
                                label="موعد الدرس"
                                prepend-icon="fas fa-calendar-day"
                                :rules="validationRules.date"
                                v-on="on"
                                readonly
                                @click:clear="date = null">
                            </v-text-field>
                        </template>
                        <v-date-picker
                            v-model="lessonVideo.showDate"
                            @change="date = false"
                        ></v-date-picker>
                        </v-menu>
                    </v-col>

                    <v-col v-if="lessonVideo.isLive == 2" cols="12">
                        <v-file-input
                            outlined=""
                            accept="video/*"
                            label="حدد الفيديو"
                            v-model="lessonVideo.file">
                        </v-file-input>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.educationYearId"
                            prepend-icon="fas fa-school"
                            :items="educationYears"
                            :rules="validationRules.required" 
                            label="السنة الدراسية" 
                            required>
                        </v-select>
                    </v-col>

                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.semesterId"
                            prepend-icon="fas fa-school"
                            :items="semesters"
                            :rules="validationRules.required" 
                            label="الفصل الدراسي" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.subjectId"
                            prepend-icon="fas fa-book"
                            :items="subjects"
                            :rules="validationRules.required" 
                            label="الماده" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.unitId"
                            prepend-icon="fas fa-book-open"
                            :items="units"
                            :rules="validationRules.required" 
                            label="الوحده" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined
                            item-text="key"
                            item-value="value"
                            v-model="lessonVideo.lessonId"
                            prepend-icon="fas fa-file-alt"
                            :items="lessons"
                            :rules="validationRules.required" 
                            label="الدرس" 
                            required>
                        </v-select>
                    </v-col>
                    <v-col md="6" cols="12">
                        <v-text-field
                            v-model="lessonVideo.name"
                            outlined
                            label="اسم الفيديو"
                            prepend-icon="fas fa-video"
                            counter="32"
                            :rules="validationRules.name">
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
    name: "AddTeacherLessonModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog'],

    data() {
        return {
            date: false,
            videoTypes: [
                {key: 'بث مباشر', value: 1}, 
                {key: 'مسجل مسبقاً', value: 2}
            ],
            educationYears: [
                {key: 'الصف الأول الإبتدائي', value: 1}
            ],
            semesters: [
                {key: 'الفصل الدراسي الأول', value: 1}
            ],
            subjects: [
                {key: 'اللغة العربية', value: 1}
            ],
            units: [
                {key: 'لغتي الجميله', value: 1}
            ],
            lessons: [
                {key: 'الدرس الأول', value: 1}
            ],
            lessonVideo: {
                isLive: null,
                name: "",
                showDate: null,
                streamKey: "",
                educationYearId: 0,
                semesterId: 0,
                subjectId: 0,
                unitId: 0,
                lessonId: 0
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