const FormData = require('form-data');
const fs = require('fs');
const axios = require('axios');

async function addEventOrganizer() {
    const event = {
        Subject: "Концерт классической музыки",
        Description: "Ежегодный концерт симфонического оркестра с программой из произведений Чайковского и Рахманинова",
        EventType: "Concert",
        Locations:
            [
                {
                    City: "Таганрог",
                    Street: "Тверская",
                    NumberOfHouse: 25,
                    DateStart: "2025-07-15T17:00:00",
                    DateEnd: "2025-07-16T17:00:00",
                    PriceOfTicket: 1000,
                    QuantityOfTickets: 150
                },
                {
                    City: "Таганрог",
                    Street: "Тверская",
                    NumberOfHouse: 25,
                    DateStart: "2025-12-15T19:00:00",
                    DateEnd: "2025-12-16T19:00:00",
                    PriceOfTicket: 500,
                    QuantityOfTickets: 50
                }
            ],
        PriceOfTicket: 100,
        QuantityOfTickets: 150,
        Tags: ["Новые горизонты"]
    };

    const formData = new FormData();

    // Добавляем простые поля
    formData.append('Subject', event.Subject);
    formData.append('Description', event.Description);
    formData.append('EventType', event.EventType);
    formData.append('Locations', JSON.stringify(event.Locations));
    event.Tags.forEach(tag => {
        formData.append('Tags', tag);
    });

    formData.append('QuantityOfTickets', event.QuantityOfTickets);

    // Добавляем изображения
    const imagePaths = [
        'images/1.jpg',
        'images/2.jpg',
        'images/3.jpg'
    ];

    imagePaths.forEach((path, index) => {
        formData.append('Images', fs.createReadStream(path), `image${index + 1}.jpg`);
    });

    try {
        const response = await axios.post('http://localhost:5555/api/event/add', formData, {
            headers: {
                ...formData.getHeaders(),
                'Accept': 'application/json',
                'Is-Test-Request-Organizer': true
            }
        });

        console.log('Событие успешно добавлено:', response.data);
    } catch (error) {

        const errors = error.response?.data?.errors;

        if (errors) {
            console.error('Ошибки валидации:');
            for (const [field, messages] of Object.entries(errors)) {
                messages.forEach(message => {
                    console.error(`- ${field}: ${message}`);
                });
            }

            console.log();

            console.error('Ошибка при добавлении события:', {
                status: error.response?.status,
                data: error.response?.data || error.message
            });
        } else {
            
        }
    }
}

addEventOrganizer();