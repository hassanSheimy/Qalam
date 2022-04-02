<template>
    <v-container class="header circle-border elevation-4 mt-4 mb-10">
        <v-text-field prepend-icon="fas fa-filter" class="py-0 my-0 px-4 mx-4"
        :label="$locales.t('search')" append-icon="fas fa-search"
        readonly @click="search = !search" hide-details :disabled="search" />
        <transition name="fade" mode="in-out">
            <v-card-actions v-if="search">
                <v-form>
                    <v-container>
                        <v-row no-gutters>
                            <v-col cols="12" md="6">
                                <v-select :no-data-text="$locales.t('noData')"
                                :items="countries" item-text="name" item-value="id" :label="$locales.t('country')" 
                                outlined prepend-icon="fas fa-globe-africa" class="ma-1" 
                                v-model="lookups.country"/>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-select :no-data-text="$locales.t('noData')"
                                :items="educationTypes" item-text="name" item-value="id" :label="$locales.t('educationType')" 
                                outlined prepend-icon="fas fa-school" class="ma-1" 
                                v-model="lookups.educationType"/>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-select :no-data-text="$locales.t('noData')"
                                :items="educationYears" item-text="name" item-value="id" :label="$locales.t('educationYear')" 
                                outlined prepend-icon="fas fa-graduation-cap" class="ma-1" 
                                v-model="lookups.educationYear"/>
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-select :no-data-text="$locales.t('noData')"
                                :items="subjects" item-text="name" item-value="id" :label="$locales.t('subject')" 
                                outlined prepend-icon="fas fa-book" class="ma-1" 
                                v-model="lookups.subject"/>
                            </v-col>
                            <v-row no-gutters>
                                <v-btn color="secondary" @click="ClearFilter">
                                    {{$locales.t('clearBtn')}}
                                </v-btn>
                                <v-btn color="error" class="mx-2" @click="search = false">
                                    {{$locales.t('cancelBtn')}}
                                </v-btn>
                                <v-spacer />
                                <v-btn color="primary" @click="ApplyFilter">
                                    {{$locales.t('searchBtn')}}
                                </v-btn>
                            </v-row>
                        </v-row>
                    </v-container>
                </v-form>
            </v-card-actions>
        </transition>
    </v-container>
</template>

<script>
import Lookups from '@/mixins/lookups.js'

export default {
    name: "FilterComponent",

    mixins: [Lookups],

    data: () => ({
        search: false,
    }),

    methods: {
        ApplyFilter(e){
            this.$emit("filter", this.lookups);
        },
        ClearFilter(){
            this.lookups.country = null;
            this.lookups.educationType = null;
            this.lookups.educationYear = null;
            this.lookups.subject = null;
        }
    },
}
</script>

<style scoped>
.circle-border{
    border-radius: 40px;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}
</style>