<template>
    <v-container fluid>
        <v-row no-gutters>
            <v-col cols="12" md="6" class="my-3">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="primary" type="Line" :height="height" class="ma-1">
                    <h4 class="title font-weight-light">رسم بياني بعدد المشاهدات</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">لأخر 12 شهر</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12" md="6" class="my-3">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="info" type="Line" :height="height" class="ma-1">
                    <h4 class="title font-weight-light">رسم بياني بعلومات الرصيد</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">كل الوقت</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12" md="6" class="my-3">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="info darken-1" type="Line" :height="height" class="ma-1">
                    <h4 class="title font-weight-light">رسم بياني بعلومات الرصيد</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">كل الوقت</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12" md="6" class="my-3">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="primary darken-1" type="Line" :height="height" class="ma-1">
                    <h4 class="title font-weight-light">رسم بياني بعلومات الرصيد</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">كل الوقت</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12" md="6">
                <rating-component class="ma-2" :data="rateData" title="التقييم العام"/>
            </v-col>

            <v-col cols="12" md="6">
                <user-opinion class="ma-2" title="اراء بعض الطلاب"/>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import ChartCard from  '@/components/cards/chart-card.vue'
import RatingComponent from '@/components/profile/rating-component.vue'
import UserOpinion from '@/components/profile/user-opinion.vue'

export default {
    name: "AdminAnalytics",

    components: {
        ChartCard,
        RatingComponent,
        UserOpinion
    },

    data: () => ({
        height: 400,
        chartLineData: {
            labels: ['0%', '10%', '20%', '30%', '40%', '50%', '60%', '70%', '80%', '90%', '100%'],
            series: [{ 
                    data: [
                        {value: 10, meta: '0%'},
                        {value: 20, meta: '10%'},
                        {value: 50, meta: '20%'},
                        {value: 10, meta: '30%'},
                        {value: 60, meta: '40%'},
                        {value: 30, meta: '50%'},
                        {value: 80, meta: '60%'},
                        {value: 35, meta: '70%'},
                        {value: 45, meta: '80%'},
                        {value: 95, meta: '90%'},
                        {value: 88, meta: '100%'},
                ]}
            ]
        },
        chartLineOptions: {
            low: 0,
            areaBase: 2,
            showArea: true,
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
            height: '400px',
            lineSmooth: false,
            chartPadding: {
                right: 0,
                left: 0
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        },
        rateData: [0, 11, 19, 30, 40],
        
    }),
}
</script>

<style scoped>

</style>