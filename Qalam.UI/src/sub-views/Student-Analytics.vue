<template>
    <v-container fluid>
        <v-row no-gutters>
            <v-col cols="12" class="ma-2">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="primary" type="Line" :height="height">
                    <h4 class="title font-weight-light">{{$locales.t('chartForSubject')}}</h4>
                    <v-col cols="12" md="4" sm="8">
                        <v-select v-model="subject"
                            :no-data-text="$locales.t('noData')"
                            item-text="name"
                            item-value="id"
                            :items="subjects" 
                            label="المادة">
                        </v-select>
                    </v-col>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">طوال العام الدراسي</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12">
                <custom-card class="v-card-profile text-right" color="primary" title="درجات الامتحانات" customClass="white--text px-5 py-3">
                    <pagination-table :headers="headers" :api="api" />
                </custom-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import ChartCard from  '@/components/cards/chart-card.vue'
import PaginationTable from '@/components/shared/pagination-table.vue'
import CustomCard from '@/components/cards/custom-card.vue'

export default {
    name: "StudentAnalytics",
    
    components: {
        ChartCard,
        PaginationTable,
        CustomCard
    },

    data: () => ({
        subjects: [
            { name: 'اللغة العربية', id: '1' }
        ],
        subject: 0,
        height: 400,
        chartLineData: {
            labels: ['0%', '10%', '20%', '30%', '40%', '50%', '60%', '70%', '80%', '90%', '100%'],
            series: [
                { data: Array.from({length: 11}, () => Math.floor(Math.random() * 100)) }
            ]
        },
        chartLineOptions: {
            low: 0,
            axisX: {
                showGrid: true,
                showLabel: true,
                offset: 15
            },
            axisY: {
                showGrid: true,
                showLabel: true,
                offset: 0
            },
            height: '400px'
        },
        rateData: [0, 11, 19, 30, 40],
        headers: [
            { text: 'التاريخ', value: 'date', sortable: true},
            { text: 'الفصل الدراسي', value: 'semesterName', sortable: false },
            { text: 'المادة', value: 'subjectName', sortable: false },
            { text: 'اسم المدرس', value: 'teacherName', sortable: false },
            { text: 'الدرجة', value: 'degree', sortable: false }
        ],

        api: {
            url: "api/exam/student-exams",
            auth: true
        }
    }),
}
</script>

<style scoped>
</style>