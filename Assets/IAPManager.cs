using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;
    public int pointGlobal;
    public string consumableID;
    
    // Start is called before the first frame update
    void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("com.scorpionsstudio.puppydog.player1", ProductType.Consumable);
        builder.AddProduct("com.scorpionsstudio.puppydog.player2", ProductType.Consumable);
        // builder.Configure<IMicrosoftConfiguration>().useMockBillingSystem = true;
        UnityPurchasing.Initialize(this, builder);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void BuyConsumable(string _locProdID)
    {
        
        consumableID = _locProdID;
        BuyProductID(_locProdID);
    }
    
    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = controller.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                controller.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Failed Purchase");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, consumableID, StringComparison.Ordinal))
        {
            print("Buy Done");
        }
        

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase Failed !");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }
    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return controller != null && extensions != null;
    }
    

}
