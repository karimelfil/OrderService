(function () {
    const originalFetch = window.fetch;

    window.fetch = async function (...args) {
        const response = await originalFetch(...args);

        const loginEndpoint = "/api/auth/login"; 

        if (args[0].includes(loginEndpoint) && response.ok) {
            const clonedResponse = response.clone();
            try {
                const data = await clonedResponse.json();
                const token = data.token;

                if (token) {
                    window.ui.preauthorizeApiKey("Bearer", token);
                    console.log(" Token automatically authorized in Swagger.");
                }
            } catch (e) {
                console.error(" Error reading token from login response", e);
            }
        }

        return response;
    };
})();