<template>
    <div>
        <v-select outlined item-text="text" item-value="value" 
        :items="$locales.t('calendarTypes')" v-model="calendarType" />

        <div class="fluid">
            <v-calendar ref="calendar" class="ma-0 bg-transparent calender-fluid" :class="{right : $vuetify.rtl}"
            :events="events" color="primary" :type="calendarType" @click:event="ShowEvent"
            :weekday-format="WeekdayFormat" :interval-format="IntervalFormat" :month-format="MonthFormat"
            :event-name="EventName" event-color="primary" event-overlap-mode="column" 
            :weekdays="[6, 0, 1, 2, 3, 4, 5]"/>
        </div>

        <v-menu v-if="selectedOpen" v-model="selectedOpen" :close-on-content-click="false"
        :activator="selectedElement" offset-x>
            <v-card color="grey lighten-4" min-width="350px" flat>
                <v-toolbar color="primary" dark>
                    <v-btn v-if="withCrud" icon @click="OpenEditModal" >
                        <v-icon>fas fa-pen</v-icon>
                    </v-btn>
                    <v-toolbar-title v-html="selectedEvent.name" />
                    <v-spacer></v-spacer>
                    <v-btn v-if="withCrud" icon @click="OpenDeleteModal">
                        <v-icon>fas fa-trash</v-icon>
                    </v-btn>
                    <v-btn v-if="!withCrud && selectedEvent.isInterval" icon @click="OpenSelectTiemModal">
                        <v-icon>fas fa-clock</v-icon>
                    </v-btn>
                    <v-btn v-if="!withCrud && !selectedEvent.isInterval && selectedEvent.canStart" icon @click="StartClick">
                        <v-icon>fas fa-play</v-icon>
                    </v-btn>
                </v-toolbar>
                <v-card-text class="black--text">
                    <p v-if="withCrud">{{$locales.t('country')}} : {{selectedEvent.country.name}}</p>
                    <p>{{$locales.t('educationYear')}} : {{selectedEvent.educationYear.name}}</p>
                    <p>{{$locales.t('startTime')}} : {{ConvertTime(selectedEvent.startTime)}}</p>
                    <p v-if="withCrud || selectedEvent.isInterval">{{$locales.t('endTime')}} : {{ConvertTime(selectedEvent.endTime)}}</p>
                    <p v-if="!withCrud && selectedEvent.isInterval" class="error--text">
                        {{$locales.t('selectInterval')}}
                    </p>
                    <span>{{selectedEvent.details}}</span>
                </v-card-text>
                <v-card-actions class="justify-end">
                    <v-btn color="sidebar" class="white--text" @click="selectedOpen = false">
                        {{$locales.t('closeBtn')}}
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-menu>
    </div>
</template>

<script>
import CustomCard from '@/components/cards/custom-card'
import moment from 'moment'

export default {
    name: "CalendarCard",

    components: {
        CustomCard,
    },

    props: {
        'with-crud': {
            type: Boolean,
            default: false
        },
        'with-filter': {
            type: Boolean,
            default: false
        },
        'events': {
            type: Array,
            required: true
        }
    },

    data: () => ({
        selectedEvent: {},
        selectedElement: null,
        selectedOpen: false,
        selectedIndex: 0,
        calendarType: 'week'
    }),

    mounted(){
        this.$refs.calendar.scrollToTime('09:00');
    },

    methods: {
        ShowEvent ({ nativeEvent, event }) {
            if (!this.selectedOpen) {
                this.selectedEvent = event
                this.selectedElement = nativeEvent.target
                var reminderInMin = Math.round(Math.abs(moment().diff(moment(this.selectedEvent.start, "YYYY-MM-DD HH:mm"))) / 60000);
                this.selectedEvent.canStart = reminderInMin <= 30;
                this.selectedIndex = this.events.indexOf(this.selectedEvent);
                this.selectedOpen = true
            }
            
            nativeEvent.stopPropagation()
        },
        MonthFormat(date){
            var i = new Date(date.date).getMonth(date)
            return this.$locales.t('months')[i].name;
        },
        WeekdayFormat(date){
            var i = new Date(date.date).getDay(date)
            return this.$locales.t('days')[(i + 1) % 7].name
        },
        IntervalFormat(interval) {
            return moment(interval.time, 'HH:mm').locale(this.$locales.current).format("hh:mmA")
        },
        ConvertTime(time){
            return moment(time, 'HH:mm').locale(this.$locales.current).format("hh:mmA");
        },
        EventName(event){
            return event.input.name;
        },
        OpenEditModal(){
            this.$emit('edit', this.selectedEvent, this.selectedIndex);
        },
        OpenDeleteModal(){
            this.$emit('delete', this.selectedEvent.id, this.selectedIndex);
        },
        OpenSelectTiemModal(){
            this.$emit('select', this.selectedEvent, this.selectedIndex);
        },
        StartClick() {
            this.$emit('start');
        }
    }
}
</script>

<style>
.right .v-calendar-daily__head{
    margin-right: 0px !important;
    margin-left: 10px !important;
}
.calender-fluid{
    width: 1050px;
}
.fluid{
    max-height: 70vh;
    overflow: auto;
}
.bg-transparent{
    background: transparent !important;
}
</style>