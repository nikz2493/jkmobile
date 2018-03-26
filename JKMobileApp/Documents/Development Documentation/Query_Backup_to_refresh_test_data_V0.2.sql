--============= Removing all other Customers except - Kirti, Sanket and Jitendra =============--

DELETE Customers
WHERE CustomerID 
	NOT IN (
			'RC0064211',--Kirti Turrey
			'RC0064212',--Sanket Prajapati
			'RC0064213',--Jitendra Garg
			'RC0064221',--Hiral Balar
			'RC0064219',--Brian Turnbull
			'RC0064220' --Jennifer Wagoner
		   )

--============= Reset registered Customers with none of terms agreed =============--
--CustomerID	Name				Passwrodhash / PasswordSalt		Email								IsCustomerRegistered	TermsAgreed
--RC0064211 	Kirti Turrey		test							kirti.turrey@mailinator.com			1						0
--RC0064212 	Sanket Prajapati	test							sanket.prajapati@mailinator.com		1						0
--RC0064213		Jitendra Garg		test							jitendra.garg@mailinator.com		1						0

UPDATE customers
SET IsCustomerRegistered = 'true',
	termsagreed='false',
	passwordhash='admin',
	passwordsalt='admin'
WHERE CustomerId 
	IN(
		'RC0064211',
		'RC0064212',
		'RC0064213',
		'RC0064221',
		'RC0064219', 
		'RC0064220' 
	  )

