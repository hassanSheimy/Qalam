export default {
    computed: {
        validationRules(){
            return {
                required: [
                    v => !!v || this.$locales.t('validation.required'),
                ],
                name: [
                    v => !!v || this.$locales.t('validation.required'),
                    v => (v && v.length <= 32) || this.$locales.t('validation.notMoreThan32'),
                ],
                number: [
                    v => !!v || this.$locales.t('validation.required'),
                    v => /^[0-9]+$/.test(v) || this.$locales.t('validation.number')
                ],
                phoneNumber: [
                    v => !!v || this.$locales.t('validation.required'),
                    v => /^[0-9]+$/.test(v) || this.$locales.t('validation.number')
                ],
                date: [
                    v => !!v || this.$locales.t('validation.required')
                ],
                email: [
                    v => !!v || this.$locales.t('validation.required'),
                    v => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) || this.$locales.t('validation.email'),
                ],
                password: [
                    v => !!v || this.$locales.t('validation.required'),
                    v => /^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$/.test(v) || this.$locales.t('validation.strongPassword')
                ],
                url: [
                ],
                atLeastOne: [
                    v => v.length > 0 || this.$locales.t('validation.required')
                ]
            }
        }
    },

    methods: {
        ValidateMatchPassword(value){
            return [
                v => v == value || this.$locales.t('validation.matchPassword')
            ]
        },
        ValidateMatch(value){
            return [
                v => v == value || `${this.$locales.t('validation.match')} ${value}`
            ]
        },
        ValidateNumberLessThan(num){
            return [
                v => !!v || this.$locales.t('validation.required'),
                v => /^[0-9]+$/.test(v) || this.$locales.t('validation.number'),
                v => parseInt(v) < num || `${this.$locales.t('validation.number')} ${(num - 1)}`
            ]
        },
        ValidateMoreThan(value){
            return [
                v => !!v || this.$locales.t('validation.required'),
                v => v > value || `${this.$locales.t('validation.valueMoreThan')} ${value}`
            ]
        },
        ValidateValueBetween(minVal, maxValue){
            return [
                v => !!v || this.$locales.t('validation.required'),
                v => v >= minVal || `${this.$locales.t('validation.valueMoreThan')} ${minVal}`,
                v => v <= maxValue || `${this.$locales.t('validation.valueLessThan')} ${maxValue}`
            ]
        }
    },
}